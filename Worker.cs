using Brendan.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EDEN.Brendan
{
    public class Worker(ILogger<Worker> logger, IConfiguration configuration) : BackgroundService
	{
		private readonly IConfiguration _configuration = configuration;
        private DiscordClient? _discordClient;

		public override async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting discord bot");

            var discordBotToken = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");

			_discordClient = new DiscordClient(new DiscordConfiguration()
            {
                Token = discordBotToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
				MinimumLogLevel = LogLevel.Information
            });

			var commands = _discordClient.UseCommandsNext(new CommandsNextConfiguration
			{
				StringPrefixes = ["!"]
			});

			commands.RegisterCommands<MyFirstModule>();

			await _discordClient.ConnectAsync();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>  Task.CompletedTask;

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
			if (_discordClient != null) {
				await _discordClient.DisconnectAsync();
				_discordClient.Dispose();
			}

			logger.LogInformation("Discord bot stopped");
        }
    }
}