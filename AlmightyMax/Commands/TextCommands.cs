using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

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
        [Description("Make Max Bark!")]
        public async Task Bark(CommandContext ctx)
        {
            await ctx.RespondAsync("Bork Bork!");
        }
    }
}