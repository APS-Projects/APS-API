using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TriviaDrunksScraper.HappyHours
{
    public class HappyHourHtmlParsers : IHappyHourHtmlParsers
    {
        private ISiteDirectory _siteDirectory;
        private HttpClient _httpClient;

        public HappyHourHtmlParsers(ISiteDirectory siteDirectory, HttpClient httpClient)
        {
            _siteDirectory = siteDirectory;
            _httpClient = httpClient;
        }
        public  async Task<IEnumerable<HtmlNode>> GetHtmlNashville()
        {
            try
            {
                var html = await _httpClient.GetStringAsync(_siteDirectory.NashvilleURL);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var happyHours = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.HasClass("basic-container"))
                    .Select(node => node.Descendants("li")).SingleOrDefault();

                var barNames = happyHours.Select(node => node.Descendants("a").SingleOrDefault().InnerHtml);



                return happyHours;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
