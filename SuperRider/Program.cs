using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperRider.DAL.Repositories.Implemantation;
using SuperRider.DAL.Repositories.Interfaces;
using SuperRider.Service.Implementation;
using SuperRider.Service.Interfaces;
using System.Threading.Tasks;
using SuperRider.Service;

namespace SuperRider
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args).UseConsoleLifetime();
            var host = builder.Build();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, config) =>
                    config
                        .AddJsonFile("appsettings.json", false, true)
                        .AddCommandLine(args)
                        .AddEnvironmentVariables())
            .ConfigureServices((_, services) =>
             {
                var connectionString = _.Configuration.GetSection("ConnectionString:Default");
                services.AddTransient<IWordCountRepository, WordCountRepository>(_ =>
                {
                    return new WordCountRepository(connectionString.Value);
                });
                 
                services.AddTransient<IWordCountService, WordCountService>();

                services.AddHostedService<BackgroundWordCountService>();
            });
    }
}
