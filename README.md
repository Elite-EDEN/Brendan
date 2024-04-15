# Brendan

Brendan is a Discord bot used by the Elite Deep-Space Explorers Network.  It is written in C# 
using .NET 8 and DSharpPlus.  Brendan is named for Brendan of Clonfert, the patron saint of
sailors and travelers.

## Contribution

To contribute to Brendan you can use any tool capable of editing C# code.  Recommended tools 
are Visual Studio, Visual Studio Code, and Rider.  Brendan is designed to be hosted in a 
container.  The production copy of Brendan is hosted on Azure in a Container Instance.  Development
work can be done using Docker Desktop.

Brendan expects an environment variable called DISCORD_BOT_TOKEN to exist and contain a valid Discord
bot token.  It is recommended that you create your own Discord bot application to obtain a token to use
in your own environment.  The production version of Brandon has a token stored in a GitHub secret that
gets passed to the production container via the CI/CD pipeline.

For a tutorial on how Brendan was initially set up, see https://swimburger.net/blog/azure/how-to-create-a-discord-bot-using-the-dotnet-worker-template-and-host-it-on-azure-container-instances.

## License

Brendan is licensed by the Elite Deep-Space Explorers Network with the MIT license.

## Production Configuration

Brendan is deployed to an Azure Container Instance using the guidance at https://learn.microsoft.com/en-us/azure/container-instances/container-instances-github-action, specifically the GitHub
Action method using a service principal.