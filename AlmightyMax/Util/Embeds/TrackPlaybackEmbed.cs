using DSharpPlus.Entities;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Util.Embeds
{
    public class TrackPlaybackEmbed : BaseEmbed
    {
        public DiscordEmbed Result { get; private set; }

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