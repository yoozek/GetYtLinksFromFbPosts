using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YtVideosFromFb.Options;
using YtVideosFromFb.Services;

namespace YtVideosFromFb
{
    class Program
    {
        static async Task Main()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();

            AppConfig appConfig = new();
            configuration.GetSection(nameof(AppConfig))
                .Bind(appConfig);
            
            var serviceProvider = new ServiceCollection()
                .AddLogging(config =>
                {
                    config.ClearProviders();
                    config.AddConfiguration(configuration.GetSection("Logging"));
                    config.AddConsole();
                })
                .AddSingleton<IFacebookService, FacebookService>()
                .AddSingleton<IFacebookApiClient, FacebookApiClient>()
                .AddSingleton(appConfig)
                .AddSingleton<IApp, App>()
                .BuildServiceProvider();
            
            var app = serviceProvider.GetService<IApp>();
            await app.Run();
        }
    }
}