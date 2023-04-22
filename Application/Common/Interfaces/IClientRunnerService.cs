using Discord;

namespace Application.Common.Interfaces;

public interface IClientRunnerService
{
    public Task RunAsync();
    public Task LogAsync(LogMessage message);
}