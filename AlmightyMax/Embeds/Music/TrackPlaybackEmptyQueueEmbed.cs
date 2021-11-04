using DSharpPlus.Entities;

namespace AlmightyMax.Embeds.Music
{
    public class TrackPlaybackEmptyQueueEmbed : BaseEmbed
    {
        public TrackPlaybackEmptyQueueEmbed()
        {
            DiscordEmbedBuilder builder = GetBuilder();

            builder.Description = "There is nothing in the queue to play!";
        }
    }
}