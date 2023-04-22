using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Interfaces;

public interface IInteractionHandler
{
    Task InitializeAsync();
    Task LogAsync(LogMessage log);
    Task ReadyAsync();
    Task HandleInteraction(SocketInteraction interaction);
}