using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using TeleRemember.Server;
using TeleRemember.Server.Interface;
using TeleRemember.Server.Webhook;
using Microsoft.Extensions.Logging;

namespace TeleRemember
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = CreateHostBuilder(args);
            WebApplication app = builder.Build();

            app.MapPost("/webhook", WebhookAPI.Webhook);
            app.MapGet("/webhook", WebhookAPI.Webhook);
            
            app.Run("http://*:80");
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
    }
}