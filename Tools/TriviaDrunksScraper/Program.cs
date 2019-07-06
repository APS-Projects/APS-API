using System;
using System.Net.Http;
using TriviaDrunksScraper.HappyHours;
using Microsoft.Extensions.DependencyInjection;

namespace TriviaDrunksScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var serviceProvider = new ServiceCollection()
                .AddTransient<HttpClient>()
                .AddTransient<ISiteDirectory, SiteDirectory>()
                .AddTransient<IHappyHourHtmlParsers, HappyHourHtmlParsers>()
                .BuildServiceProvider();

            var happyHourParsers = serviceProvider.GetService<IHappyHourHtmlParsers>();

            happyHourParsers.GetHtmlNashville();

            serviceProvider.Dispose();
        }

    }
}
