using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using TeleRemember.Server;
using TeleRemember.Server.Interface;
using Microsoft.AspNetCore.Hosting;

namespace TeleRemember
{
    public static class Program
    {
        private static IHost? _host;

        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = CreateHostBuilder(args);
            _host = builder.Build();

            _host.Run();
        }

        private static WebApplicationBuilder CreateHostBuilder(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            IServiceCollection host = builder.Services;

            host.AddSingleton<Config>();
            host.AddSingleton<HttpSharedClient>();
            host.AddLogging();
            host.AddHostedService<App>();

            host.AddSingleton<Bot>();
            host.AddTransient<ITelegramAPI, TelegramAPI>();

            return builder;

            //IHostBuilder builder = Host.CreateDefaultBuilder(args);
            //builder.ConfigureServices((context, services) =>
            //{
            //    services.AddSingleton<Config>();
            //    services.AddSingleton<HttpSharedClient>();
            //    services.AddLogging();
            //    services.AddHostedService<App>();

            //    services.AddSingleton<Bot>();
            //    services.AddTransient<ITelegramAPI, TelegramAPI>();
            //});

            //return builder;
        }

        public static IHost? GetInstace
        {
            get { return _host; }
        }
    }
}