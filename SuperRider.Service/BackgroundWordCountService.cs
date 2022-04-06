using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SuperRider.Service.Interfaces;

namespace SuperRider.Service
{
    public class BackgroundWordCountService : BackgroundService
    {
        private readonly IWordCountService _wordCountService;
        private readonly IConfiguration _configuration;

        public BackgroundWordCountService(IWordCountService wordCountService, IConfiguration configuration)
        {
            _wordCountService = wordCountService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("BackgroundWordCountService.ExecuteAsync");
            while (!stoppingToken.IsCancellationRequested)
            {
                var filePath = _configuration.GetSection("FilePath");
                await _wordCountService.SaveFilteredWordsAsync(filePath.Value);
                await Task.Delay(5000);
            }
            Console.WriteLine("BackgroundWordCountService.ExecuteAsync token cancelled");
        }
    }
}
