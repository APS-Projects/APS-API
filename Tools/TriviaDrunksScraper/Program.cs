using System;
using System.Net.Http;
using TriviaDrunksScraper.HappyHours;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using TriviaDrunksScraper.Repositories;
using TriviaDrunksScraper.HappyHours.Cities;

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
                .AddTransient<TriviaDrunkRepository>()
                .AddTransient<ParserConfig>()
                .BuildServiceProvider();

            var parserConfig = serviceProvider.GetService<ParserConfig>();

            var nashvilleHtml = new NashvilleHtmlParser(parserConfig);

            nashvilleHtml.ParseHappyHourHtml().Wait();
            Console.ReadLine();
            serviceProvider.Dispose();
        }
    }
}
