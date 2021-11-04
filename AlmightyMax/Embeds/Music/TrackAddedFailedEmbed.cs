using DSharpPlus.Entities;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Embeds.Music
{
    public class TrackAddedFailedEmbed : BaseEmbed
    {
        public TrackAddedFailedEmbed(LavalinkTrack track)
        {
            DiscordEmbedBuilder builder = GetBuilder();

            builder.Color = DiscordColor.Red;
            builder.Description = $"Failed trying to add [{track.Title}]({track.Uri.ToString()}) to the queue.";
        }
    }
}