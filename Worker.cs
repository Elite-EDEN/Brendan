using Discord.WebSocket;
using Discord;
using Discord.Commands;
using System.Reflection;
using EDEN.Brendan.Commands;

namespace EDEN.Brendan
{
    public class Worker(ILogger<Worker> logger, IConfiguration configuration) : BackgroundService
	{
		private readonly IConfiguration _configuration = configuration;
		private static DiscordSocketClient? _client;
		private static CommandService? _commands;
		private static IServiceProvider? _services;

		public override async Task StartAsync(CancellationToken cancellationToken)
        {
			_client = new DiscordSocketClient(new DiscordSocketConfig {
				LogLevel = LogSeverity.Info
			});

			_commands = new CommandService(new CommandServiceConfig
			{
				LogLevel = LogSeverity.Info,
				CaseSensitiveCommands = false,
			});

			_services = ConfigureServices();

			_client.Log += Log;
			_commands.Log += Log;

			logger.LogInformation("Initializing commands");
			await InitCommands();

			// Login and connect.
			logger.LogInformation("Connecting discord bot");
			await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN"));
			await _client.StartAsync();

			// Wait infinitely so your bot actually stays connected.
			logger.LogInformation("Starting discord bot");
			await Task.Delay(Timeout.Infinite, cancellationToken);
		}

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>  Task.CompletedTask;

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
			if (_client != null) {
				await _client.StopAsync();
				await _client.DisposeAsync();
			}
			logger.LogInformation("Discord bot stopped");
        }

		// If any services require the client, or the CommandService, or something else you keep on hand,
		// pass them as parameters into this method as needed.
		// If this method is getting pretty long, you can separate it out into another file using partials.
		private static IServiceProvider ConfigureServices()
		{
			var serviceCollection = new ServiceCollection();
			//serviceCollection.AddSingleton(new SomeServiceClass());
			return serviceCollection.BuildServiceProvider();
		}

		private static async Task InitCommands()
		{
			if (_client == null) throw new InvalidOperationException("Discord client is null");
			if (_commands == null) throw new InvalidOperationException("Command service is null");
			// Either search the program and add all Module classes that can be found.
			// Module classes MUST be marked 'public' or they will be ignored.
			// You also need to pass your 'IServiceProvider' instance now,
			// so make sure that's done before you get here.
			await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
			
			// Subscribe a handler to see if a message invokes a command.
			_client.MessageReceived += HandleCommandAsync;
		}

		private static async Task HandleCommandAsync(SocketMessage arg)
		{
			if (_client == null) throw new InvalidOperationException("Discord client is null");
			if (_commands == null) throw new InvalidOperationException("Command service is null");

			// Bail out if it's a System Message.
			if (arg is not SocketUserMessage msg) return;

			// We don't want the bot to respond to itself or other bots.
			if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;

			// Commands are invoked by mentioning the bot first.
			var pos = 0;
			if (msg.HasMentionPrefix(_client.CurrentUser, ref pos))
			{
				// Create a Command Context.
				var context = new SocketCommandContext(_client, msg);

				// Execute the command. (result does not indicate a return value, 
				// rather an object stating if the command executed successfully).
				var result = await _commands.ExecuteAsync(context, pos, _services);

				// This does not catch errors from commands with 'RunMode.Async',
				// subscribe a handler for '_commands.CommandExecuted' to see those.
				if (!result.IsSuccess && result.Error != CommandError.UnknownCommand) {
					await msg.Channel.SendMessageAsync(result.ErrorReason);
				}
			}
		}

		private Task Log(LogMessage msg)
		{
			switch (msg.Severity) {
				case LogSeverity.Critical:
					logger.LogCritical(msg.Message);
					break;
				case LogSeverity.Error:
					logger.LogError(msg.Message);
					break;
				case LogSeverity.Warning:
					logger.LogWarning(msg.Message);
					break;
				case LogSeverity.Info:
					logger.LogInformation(msg.Message);
					break;
				case LogSeverity.Verbose:
					logger.LogTrace(msg.ToString());
					break;
				case LogSeverity.Debug:
					logger.LogDebug(msg.ToString());
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return Task.CompletedTask;
		}
	}
}