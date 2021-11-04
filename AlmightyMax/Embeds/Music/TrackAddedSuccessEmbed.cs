using DSharpPlus.Entities;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Embeds.Music
{
    public class TrackAddedSuccessEmbed : BaseEmbed
    {
        public TrackAddedSuccessEmbed(LavalinkTrack track)
        {
            DiscordEmbedBuilder builder = GetBuilder();
            
            builder.Description = $"[{track.Title}]({track.Uri.ToString()}) was successfully added to the queue!";
            builder.AddField("Song Length", track.Length.ToString());
        }
    }
}