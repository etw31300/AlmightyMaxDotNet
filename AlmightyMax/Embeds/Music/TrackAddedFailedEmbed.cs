using DSharpPlus.Entities;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Embeds.Music
{
    public class TrackAddedFailedEmbed : BaseEmbed
    {
        public TrackAddedFailedEmbed(LavalinkTrack track)
        {
            DiscordEmbedBuilder builder = Builder;

            builder.Color = DiscordColor.Red;
            builder.Url = track.Uri.ToString();
            builder.Description = $"Failed trying to add {track.Title} to the queue.";

            Result = builder.Build();
        }
    }
}