# Brendan

Brendan is a Discord bot used by the Elite Deep-Space Explorers Network.  It is written in C# 
using .NET 8 and DSharpPlus.  Brendan is named for Brendan of Clonfert, the patron saint of
sailors and travelers.

## Contribution

To contribute to Brendan you can use any tool capable of editing C# code.  Recommended tools 
are Visual Studio, Visual Studio Code, and Rider.  Brendan is designed to be hosted on any Linux server.  His production environment
is a droplet on Digital Ocean.

Brendan expects an environment variable called DISCORD_BOT_TOKEN to exist and contain a valid Discord
bot token.  It is recommended that you create your own Discord bot application to obtain a token to use
in your own environment.

For a tutorial on how Brendan was initially set up, see https://amelspahic.com/deploy-net-6-application-with-github-actions-to-self-hosted-linux-machine-virtual-private-server-raspberry-pi

## License

Brendan is licensed by the Elite Deep-Space Explorers Network with the MIT license.

## Production Configuration

Brendan is deployed to a VPS droplet on Digital Ocean via a local Github action runner.
