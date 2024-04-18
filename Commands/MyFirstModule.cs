// ReSharper disable UnusedMember.Global

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Brendan.Commands
{
	public class MyFirstModule : BaseCommandModule
	{
		[Command("greet")]
		public static async Task GreetCommand(CommandContext ctx, DiscordMember member)
		{
			await ctx.RespondAsync($"Greetings, {member.Mention}! Enjoy the mention!");
		}

		[Command("random")]
		public static async Task RandomCommand(CommandContext ctx, int min, int max)
		{
			var random = new Random();
			await ctx.RespondAsync($"Your number is: {random.Next(min, max)}");
		}
	}
}
