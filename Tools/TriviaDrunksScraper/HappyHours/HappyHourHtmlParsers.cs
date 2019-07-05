using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TriviaDrunksScraper.HappyHours
{
    public class HappyHourHtmlParsers
    {
        private readonly ISiteDirectory _siteDirectory;
        public HappyHourHtmlParsers(ISiteDirectory siteDirectory)
        {
            _siteDirectory = siteDirectory;
        }
        public  async void GetHtmlNashville(string siteURL, HttpClient httpClient)
        {

        }
    }
}
