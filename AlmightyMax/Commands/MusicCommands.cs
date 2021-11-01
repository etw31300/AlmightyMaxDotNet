using System;
using System.Linq;
using System.Threading.Tasks;
using AlmightyMax.Util.Embeds;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Commands
{
    public class MusicCommands : BaseCommandModule
    {
        [Command("play")]
        [Aliases("p")]
        [Description("Make Max play music")]
        public async Task Play(CommandContext ctx, [RemainingText] string searchQuery)
        {
            var lavalink = ctx.Client.GetLavalink();

            if (!lavalink.ConnectedNodes.Any())
            {
                await Console.Error.WriteLineAsync("Lavalink connection not established");
                return;
            }

            //Get the voice channel that the invoking user is in
            DiscordChannel vc = ctx.Member.VoiceState.Channel;

            if (vc == null)
            {
                await ctx.RespondAsync("You need to be connected to a voice channel to do that.");
                return;
            }
            
            var lavaNode = lavalink.ConnectedNodes.Values.First();
            
            //Get the guild connection or connect if one doesn't exist
            var connection = lavalink.GetGuildConnection(vc.Guild) ?? await lavaNode.ConnectAsync(vc);

            var trackLoadResult = await lavaNode.Rest.GetTracksAsync(searchQuery);

            if (trackLoadResult.LoadResultType is LavalinkLoadResultType.LoadFailed or LavalinkLoadResultType.NoMatches)
            {
                await ctx.RespondAsync($"Failed to load track {searchQuery}");
                await Console.Error.WriteLineAsync($"Error loading Lavalink Track: {trackLoadResult.Exception.Message}");
                return;
            }

            var track = trackLoadResult.Tracks.First();
            
            await connection.PlayAsync(track);

            await ctx.RespondAsync(new TrackPlaybackEmbed(track).Result);
        }
    }
}