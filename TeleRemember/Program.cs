using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using TeleRemember.Server;
using TeleRemember.Server.Interface;
using TeleRemember.Server.Webhook;
using Microsoft.Extensions.Logging;
using TeleRemember.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TeleRemember
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = CreateHostBuilder(args);
            WebApplication app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var webhookAPI = scope.ServiceProvider.GetRequiredService<WebhookAPI>();
                app.MapPost("/webhook", webhookAPI.Webhook);
            }

            app.Run("http://*:80");
        }

        private static WebApplicationBuilder CreateHostBuilder(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            IServiceCollection host = builder.Services;

            builder.Configuration.AddUserSecrets<App>();
            host.AddSingleton<Config>();
            host.AddDbContext<ListDbContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer") ??
                    throw new NullReferenceException("Connection string is missing.")));
            host.AddSingleton<HttpSharedClient>();
            host.AddLogging();
            host.AddHostedService<App>();

            host.AddSingleton<Bot>();
            host.AddTransient<ITelegramAPI, TelegramAPI>();
            host.AddTransient<WebhookAPI>();

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