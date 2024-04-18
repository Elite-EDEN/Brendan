using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Brendan.Commands
{
	public class MyFirstModule : BaseCommandModule
	{
		[Command("greet")]
		public async Task GreetCommand(CommandContext ctx)
		{
			await ctx.RespondAsync("Greetings! Thank you for executing me!");
		}
	}
}
