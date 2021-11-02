using DSharpPlus.Entities;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Embeds.Music
{
    public class TrackPlaybackEmbed : BaseEmbed
    {
        public TrackPlaybackEmbed(LavalinkTrack track)
        {
            DiscordEmbedBuilder builder = Builder;

            builder.Url = track.Uri.ToString();
            builder.Description = $"Now playing {track.Title} by {track.Author}";
            builder.AddField("Song Length", track.Length.ToString());

            Result = builder.Build();
        }
    }
}