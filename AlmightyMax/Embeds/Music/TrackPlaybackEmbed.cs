using DSharpPlus.Entities;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Embeds.Music
{
    public class TrackPlaybackEmbed : BaseEmbed
    {
        public TrackPlaybackEmbed(LavalinkTrack track)
        {
            DiscordEmbedBuilder builder = GetBuilder();
            
            builder.Thumbnail.Url = GetYoutubeThumbnailUrl(track.Identifier);
            builder.Title = "Now playing";
            builder.Description = $"[{track.Title}]({track.Uri.ToString()})";
            builder.AddField("Song Length", track.Length.ToString());
        }
    }
}