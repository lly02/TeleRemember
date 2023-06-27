using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeleRemember.Server;
using TeleRemember.Server.Interface;

namespace TeleRemember
{
    public static class Program
    {
        private static IHost? _host;

        public static void Main(string[] args)
        {
            IHostBuilder builder = CreateHostBuilder(args);
            _host = builder.Build();

            _host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<Config>();
                services.AddSingleton<HttpSharedClient>();
                services.AddLogging();
                services.AddHostedService<App>();

                services.AddSingleton<Bot>();
                services.AddTransient<ITelegramAPI, TelegramAPI>();
            });

            return builder;
        }

        public static IHost? GetInstace
        {
            get { return _host; }
        }
    }
}