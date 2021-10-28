using System;
using System.Linq;
using System.Threading.Tasks;
using AlmightyMax.Config;
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
                Console.Error.WriteLine("Lavalink connection not established");
                return;
            }

            DiscordChannel vc = ctx.Member?.VoiceState?.Channel;

            if (vc == null)
            {
                await ctx.RespondAsync("You need to be connected to a voice channel to do that.");
                return;
            }
            
            
        }
    }
}