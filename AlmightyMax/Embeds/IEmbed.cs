using DSharpPlus.Entities;

namespace AlmightyMax.Embeds
{
    public interface IEmbed
    {
        public DiscordEmbedBuilder GetBuilder();
        public DiscordEmbed GetResult();
    }
}