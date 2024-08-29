using Discord.Commands;

namespace EDEN.Brendan.Commands
{
	public class SampleCommands : ModuleBase<SocketCommandContext>
	{
		// @Brendan say hello world -> hello world
		[Command("say")]
		[Summary("Echoes a message.")]
		public Task SayAsync([Remainder][Summary("The text to echo")] string echo)
			=> ReplyAsync(echo);

		// ReplyAsync is a method on ModuleBase 
	}
}
