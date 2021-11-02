using DSharpPlus.Entities;

namespace AlmightyMax.Embeds
{
    public interface IEmbed
    {
        public DiscordEmbedBuilder Builder { get; set; }
        public DiscordEmbed Result { get; protected set; }
    }
}