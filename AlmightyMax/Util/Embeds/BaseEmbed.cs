﻿using System;
using DSharpPlus.Entities;

namespace AlmightyMax.Util.Embeds
{
    public class BaseEmbed
    {
        public const string MaxColor = "#765c4d";
        public const string MaxName = "Almighty Max";
        public const string MaxIconUrl = "https://cdn.discordapp.com/app-icons/345646168933531658/688450b718b78955323209757e55a281.png?size=256";
        
        public DiscordEmbedBuilder Builder { get; protected set; }

        public BaseEmbed()
        {
            Builder = new DiscordEmbedBuilder
            {
                Color = new DiscordColor(MaxColor),
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    Name = MaxName,
                    IconUrl = MaxIconUrl
                },
                Timestamp = DateTimeOffset.Now
            };
        }
    }
}