using System;
using System.Linq;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using AlmightyMax.Embeds;
using AlmightyMax.Embeds.Music;
using AlmightyMax.Music.Lavalink;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus.Lavalink.EventArgs;
using Emzi0767.Utilities;

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
            var vc = ctx.Member.VoiceState.Channel;

            if (vc == null)
            {
                await ctx.RespondAsync("You need to be connected to a voice channel to do that.");
                return;
            }
            
            var lavaNode = lavalink.ConnectedNodes.Values.First();
            
            //Get the guild connection or connect if one doesn't exist
            var guildConnection = lavalink.GetGuildConnection(vc.Guild) ?? await lavaNode.ConnectAsync(vc);

            var trackLoadResult = await lavaNode.Rest.GetTracksAsync(searchQuery);

            if (trackLoadResult.LoadResultType is LavalinkLoadResultType.LoadFailed or LavalinkLoadResultType.NoMatches)
            {
                await ctx.RespondAsync($"Failed to load track {searchQuery}");
                await Console.Error.WriteLineAsync($"Error loading Lavalink Track: {trackLoadResult.Exception.Message}");
                return;
            }

            var track = trackLoadResult.Tracks.First();
            
            //if a track is already playing
            if (guildConnection.CurrentState.CurrentTrack != null)
            {
                var returnedTrack = await LavalinkQueuesManager.AddTrack(vc.GuildId.GetValueOrDefault(), track);
                if (returnedTrack != null)
                    await ctx.RespondAsync(new TrackAddedSuccessEmbed(returnedTrack).GetResult());
                else
                    await ctx.RespondAsync(new TrackAddedFailedEmbed(track).GetResult());
            }
            else
            {
                await guildConnection.PlayAsync(track);
                await ctx.RespondAsync(new TrackPlaybackEmbed(track).GetResult());
            }

            guildConnection.DiscordWebSocketClosed += (sender, args) => LavalinkEventHandlers.GuildConnectionOnWebSocketClosed(sender, args, ctx);

            guildConnection.PlaybackFinished -= (sender, args) => LavalinkEventHandlers.GuildConnectionOnPlaybackFinished(sender, args, ctx);
            guildConnection.PlaybackFinished += (sender, args) =>  LavalinkEventHandlers.GuildConnectionOnPlaybackFinished(sender, args, ctx);
        }


        [Command("skip")]
        [Description("Skips the current song to the next in the queue")]
        public async Task Skip(CommandContext ctx)
        {
            var lavalink = ctx.Client.GetLavalink();
            
            if (!lavalink.ConnectedNodes.Any())
            {
                await Console.Error.WriteLineAsync("Lavalink connection not established");
                return;
            }
            
            //Get the voice channel that the invoking user is in
            var vc = ctx.Member.VoiceState.Channel;

            if (vc == null)
            {
                await ctx.RespondAsync("You need to be connected to a voice channel to do that.");
                return;
            }

            var lavaNode = lavalink.ConnectedNodes.Values.First();

            var guildConnection = lavaNode.GetGuildConnection(vc.Guild);
            
            if (guildConnection == null)
            {
                await ctx.RespondAsync("I am not currently in a voice channel. You must play music first!");
                return;
            }

            var track = await LavalinkQueuesManager.Seek(vc.GuildId.GetValueOrDefault());

            if (track == null)
            {
                await ctx.RespondAsync(new TrackPlaybackEmptyQueueEmbed().GetResult());
            }
            else
            {
                await guildConnection.PlayAsync(track);
                await ctx.RespondAsync(new TrackPlaybackEmbed(track).GetResult());
            }
        }
    }
}