using DSharpPlus;
using DSharpPlus.EventArgs;

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
                Intents = DiscordIntents.All
            });

            _discordClient.MessageCreated += OnMessageCreated;
            await _discordClient.ConnectAsync();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>  Task.CompletedTask;

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
			if (_discordClient != null) {
				_discordClient.MessageCreated -= OnMessageCreated;
				await _discordClient.DisconnectAsync();
				_discordClient.Dispose();
			}

			logger.LogInformation("Discord bot stopped");
        }

        private async Task OnMessageCreated(DiscordClient client, MessageCreateEventArgs e)
        {
            if (e.Message.Content.StartsWith("ping", StringComparison.OrdinalIgnoreCase))
            {
                logger.LogInformation("pinged, responding with pong!");
                await e.Message.RespondAsync("Pong...");
            }
        }
    }
}