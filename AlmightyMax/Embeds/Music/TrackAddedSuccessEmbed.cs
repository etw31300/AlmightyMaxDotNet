using DSharpPlus.Entities;
using DSharpPlus.Lavalink;

namespace AlmightyMax.Embeds.Music
{
    public class TrackAddedSuccessEmbed : BaseEmbed
    {
        public TrackAddedSuccessEmbed(LavalinkTrack track)
        {
            DiscordEmbedBuilder builder = Builder;

            builder.Url = track.Uri.ToString();
            builder.Description = $"{track.Title} was successfully added to the queue!";
            builder.AddField("Song Length", track.Length.ToString());

            Result = builder.Build();
        }
    }
}