using System;
using System.Net.Http;
using TriviaDrunksScraper.HappyHours;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace TriviaDrunksScraper
{
    class Program
    {
        static void  Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(configuration)
                .AddTransient<HttpClient>()
                .AddTransient<ISiteDirectory, SiteDirectory>()
                .AddTransient<IHappyHourHtmlParsers, HappyHourHtmlParsers>()
                .BuildServiceProvider();

            var happyHourParsers = serviceProvider.GetService<IHappyHourHtmlParsers>();

            happyHourParsers.GetHtmlNashville().Wait();
            Console.ReadLine();
            serviceProvider.Dispose();
        }
    }
}
