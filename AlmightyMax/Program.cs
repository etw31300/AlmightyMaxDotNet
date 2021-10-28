using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AlmightyMax.Commands;
using AlmightyMax.Config;
using AlmightyMax.Domain.Configuration;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlmightyMax
{
    internal static class Program
    {
        private static DiscordClient _discordClient;
        private static DiscordRestClient _restClient;
        private static CommandsNextExtension _commandsNextExtension;
        private static InteractivityExtension _interactivityExtension;
        private static VoiceNextExtension _voiceNextExtension;
        private static LavalinkExtension _lavalinkExtension;
        
        public static LavalinkNodeConnection LavalinkNodeConnection { get; private set; }

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }


        /// <summary>
        /// Main method that will initialize the client
        /// </summary>
        /// <param name="args">
        /// Runtime arguments
        /// </param>
        static async Task MainAsync(string[] args)
        {
            #region client-specific configurations
            IConfiguration maxConfig = AlmightyMaxConfig.UseDefault;
            
            //Lavalink
            LavalinkConfiguration lavalinkConfig = LavalinkConfig.UseDefault;
            LavalinkConfig.InitializeLavalinkProcess(); //initializes the Lavalink process
            #endregion

            #region configure client components
            _discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = maxConfig["AlmightyMaxSecrets:Token"],
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug
            });

            _commandsNextExtension = _discordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = maxConfig.GetSection("AlmightyMax:Prefixes").Get<string[]>(),
                EnableDms = false,
                CaseSensitive = false
            });

            _interactivityExtension = _discordClient.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(1),
                PollBehaviour = PollBehaviour.DeleteEmojis
            });

            _voiceNextExtension = _discordClient.UseVoiceNext(new VoiceNextConfiguration
            {
                AudioFormat = AudioFormat.Default,
                EnableIncoming = false
            });

            _lavalinkExtension = _discordClient.UseLavalink();

            #endregion

            #region command registration
            _commandsNextExtension.RegisterCommands<TextCommands>();
            _commandsNextExtension.RegisterCommands<MusicCommands>();
            #endregion

            #region connect client and keep alive
            
            await _discordClient.ConnectAsync();
            LavalinkNodeConnection = await _lavalinkExtension.ConnectAsync(lavalinkConfig);
            await Task.Delay(-1);
            
            #endregion
        }
    }
}