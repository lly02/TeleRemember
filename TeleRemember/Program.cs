using Microsoft.Extensions.Hosting;

namespace TeleRemember
{
    public class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            IHost host = builder.Build();
            host.Run();
        }

        private static IHostBuilder CreateHostBuilder()
        {

        }
    }
}