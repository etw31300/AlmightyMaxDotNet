using System;
using System.Threading.Tasks;
using AlmightyMax.Embeds.Music;
using DSharpPlus.CommandsNext;
using DSharpPlus.Lavalink;
using DSharpPlus.Lavalink.EventArgs;

namespace AlmightyMax.Music.Lavalink
{
    public static class LavalinkEventHandlers
    {
        public static async Task GuildConnectionOnWebSocketClosed(LavalinkGuildConnection sender, WebSocketCloseEventArgs args, CommandContext ctx)
        {
            await LavalinkQueuesManager.ClearQueue(sender.Guild.Id);
        }
        
        public static async Task GuildConnectionOnPlaybackFinished(LavalinkGuildConnection sender, TrackFinishEventArgs e, CommandContext ctx)
        {
            var nextTrack = await LavalinkQueuesManager.Seek(sender.Guild.Id);
            if (nextTrack != null)
            {
                await sender.PlayAsync(nextTrack);
                await ctx.RespondAsync(new TrackPlaybackEmbed(nextTrack).GetResult());
            }
        }
    }
}