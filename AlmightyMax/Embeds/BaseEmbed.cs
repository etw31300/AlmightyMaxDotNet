using System;
using DSharpPlus.Entities;

namespace AlmightyMax.Embeds
{
    public abstract class BaseEmbed : IEmbed
    {
        private const string MaxColor = "#765c4d";
        private const string MaxName = "Almighty Max";
        private const string MaxIconUrl = "https://cdn.discordapp.com/app-icons/345646168933531658/688450b718b78955323209757e55a281.png?size=256";

        private readonly DiscordEmbedBuilder _builder;
        
        protected string GetYoutubeThumbnailUrl(string trackId) => $"https://img.youtube.com/vi/{trackId}/1.jpg";

        protected BaseEmbed()
        {
            _builder = new DiscordEmbedBuilder
            {
                Color = new DiscordColor(MaxColor),
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    Name = MaxName,
                    IconUrl = MaxIconUrl
                },
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail(),
                Timestamp = DateTimeOffset.Now
            };
        }

        public DiscordEmbedBuilder GetBuilder()
        {
            return _builder;
        }

        public DiscordEmbed GetResult()
        {
            return GetBuilder().Build();
        }
    }
}