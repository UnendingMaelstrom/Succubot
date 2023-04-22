
using Application.Common.Interfaces;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Infrastructure.Handlers;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        private static readonly DiscordSocketConfig _socketConfig = new()
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
            AlwaysDownloadUsers = true,
        };
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
        
            EntityFrameworkServiceCollectionExtensions.AddDbContext<ApplicationDbContext>(services, options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            
            ServiceCollectionServiceExtensions.AddScoped<IApplicationDbContext>(services, provider => provider.GetRequiredService<ApplicationDbContext>());
            ServiceCollectionServiceExtensions.AddScoped<ApplicationDbContextInitializer>(services);
            ServiceCollectionServiceExtensions.AddSingleton(services, _socketConfig);

            ServiceCollectionServiceExtensions.AddSingleton<DiscordSocketClient>(services);
            ServiceCollectionServiceExtensions.AddSingleton(services,
                x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));

           ServiceCollectionServiceExtensions.AddSingleton<IInteractionHandler, InteractionHandler>(services);
           ServiceCollectionServiceExtensions.AddSingleton<IClientRunnerService, ClientRunnerService>(services);
            
            return services;
        }
    }
}

