using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TeleRemember.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleRemember
{
    public class App : IHostedService
    {
        private readonly ILogger _logger;
        private readonly Bot _bot;
        private readonly Config _config;

        public App(
            Config config,
            Bot bot,
            ILogger<App> logger,
            IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _bot = bot;

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("App has started.");

            _bot.InitAsync();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("App has stopped.");

            return Task.CompletedTask;
        }

        private void OnStarted()
        {

        }

        private void OnStopping()
        {
        }

        private void OnStopped()
        {
        }
    }
}
