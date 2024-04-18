using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

// Disable the unused member warning--ReSharper can't figure out that there is reflection in Worker.cs that will use this code
// ReSharper disable UnusedMember.Global
// Disable the compiler warning about making these methods static.  It doesn't work, but the compiler can't figure out the runtime reflection.
#pragma warning disable CA1822

namespace Brendan.Commands
{
	public class MyFirstModule : BaseCommandModule
	{
		public Random? Rng { private get; set; } // Implied public setter.

		[Command("greet")]
		public async Task GreetCommand(CommandContext ctx, DiscordMember member)
		{
			await ctx.RespondAsync($"Greetings, {member.Mention}! Enjoy the mention!");
		}

		[Command("random")]
		public async Task RandomCommand(CommandContext ctx, int min, int max)
		{
			if (Rng != null) await ctx.RespondAsync($"Your number is: {Rng.Next(min, max)}");
		}
	}
}
