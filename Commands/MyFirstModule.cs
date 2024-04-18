using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Brendan.Commands
{
	public class MyFirstModule : BaseCommandModule
	{
		[Command("greet")]
		public async Task GreetCommand(CommandContext ctx, string name)
		{
			await ctx.RespondAsync($"Greetings, {name}! You're pretty neat!");
		}
	}
}
