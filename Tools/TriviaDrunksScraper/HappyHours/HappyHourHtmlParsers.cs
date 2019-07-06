using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TriviaDrunksScraper.HappyHours
{
    public class HappyHourHtmlParsers : IHappyHourHtmlParsers
    {
        private readonly ISiteDirectory _siteDirectory;
        private readonly HttpClient _httpClient;

        public HappyHourHtmlParsers(ISiteDirectory siteDirectory, HttpClient httpClient)
        {
            _siteDirectory = siteDirectory;
            _httpClient = httpClient;
        }
        public  async void GetHtmlNashville()
        {

        }
    }
}
