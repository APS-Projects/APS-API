using System;
using System.Net.Http;
using TriviaDrunksScraper.HappyHours;

namespace TriviaDrunksScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var siteDirectory = new SiteDirectory();
            var httpClient = new HttpClient();
        }
    }
}
