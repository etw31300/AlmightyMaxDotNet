using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace AlmightyMax.Commands
{
    public class TextCommands : BaseCommandModule
    {
        /// <summary>
        /// Bark command that responds to caller with the message "Bork Bork!"
        /// </summary>
        /// <param name="ctx">
        /// Command Context
        /// </param>
        [Command("bark")]
        [Aliases("bork")]
        [Description("Make Max bark")]
        public async Task Bark(CommandContext ctx)
        {
            await ctx.RespondAsync("Bork Bork!");
        }

        /// <summary>
        /// Command that gets Max to tell you a joke
        /// </summary>
        /// <param name="ctx">
        /// Command Context
        /// </param>
        [Command("joke")]
        [Description("Make Max tell you a joke")]
        public async Task Joke(CommandContext ctx)
        {
            DiscordMessage question = await ctx.RespondAsync("What happened when the dog crossed the road?");
            await ctx.TriggerTypingAsync();
            await Task.Delay(TimeSpan.FromSeconds(3));
            await question.RespondAsync("It got ran over! :laughing:");
        }
    }
}