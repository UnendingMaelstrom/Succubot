using Application.Common.Interfaces;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class ClientRunnerService : IClientRunnerService
{
    private DiscordSocketClient _client;
    private IInteractionHandler _handler;
    private IConfiguration _config;

    public ClientRunnerService(DiscordSocketClient client, IInteractionHandler handler, IConfiguration config)
    {
        _client = client;
        _handler = handler;
        _config = config;
    }

    public async Task RunAsync()
    {
        _client.Log += LogAsync;

        await _handler.InitializeAsync();

        // Here we can initialize the service that will register and execute our commands

       
        await _client.LoginAsync(TokenType.Bot, _config.GetValue<string>("Discord:Token"));
        await _client.StartAsync();

        // Never quit the program until manually forced to.
        await Task.Delay(Timeout.Infinite);
    }

    public async Task LogAsync(LogMessage message)
        => Console.WriteLine(message.ToString());
}