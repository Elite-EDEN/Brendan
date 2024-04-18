using DSharpPlus;
using DSharpPlus.CommandsNext;
using System.Reflection;

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

			var services = new ServiceCollection()
						   .AddSingleton<Random>()
						   .BuildServiceProvider();

			var commands = _discordClient.UseCommandsNext(new CommandsNextConfiguration
			{
				StringPrefixes = ["!"],
				Services = services
			});

			commands.RegisterCommands(Assembly.GetExecutingAssembly());

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