using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TriviaDrunksScraper.Repositories;

namespace TriviaDrunksScraper.HappyHours.Cities
{
    public class NashvilleHtmlParser : IHtmlParser
    {
        private ISiteDirectory _siteDirectory;
        private HttpClient _httpClient;
        private TriviaDrunkRepository _repository;

        public NashvilleHtmlParser(ParserConfig parserConfig)
        {
            _siteDirectory = parserConfig.SiteDirectory;
            _httpClient = parserConfig.HttpClient;
            _repository = parserConfig.Repository;
        }
        public  async Task<IEnumerable<HtmlNode>> ParseHappyHourHtml()
        {
            try
            {
                //Most next three lines out
                var html = await _httpClient.GetStringAsync(_siteDirectory.NashvilleURL);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var happyHours = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.HasClass("basic-container"))
                    .Select(node => node.Descendants("li")).SingleOrDefault();

                var parsedBarNames = ParseBarNames(happyHours);

                var happyHourCopy = new List<HtmlNode>(happyHours);

                var parsedDescriptions = ParseHappyHourDescription(happyHourCopy);

                _repository.UpdateBars(parsedBarNames, 1);

                _repository.UpdateHappyHours(parsedDescriptions, 1);

                return happyHours;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public Task<IEnumerable<HtmlNode>> ParseTriviaHtml ()
        //{
        //}

        private IEnumerable<string> ParseBarNames(IEnumerable<HtmlNode> happyHours)
        {
            try
            {
                var barNames = happyHours.Select(node => node.Descendants("a").SingleOrDefault().InnerHtml);

                var parsedBarNames = new List<string>();

                foreach (var item in barNames)
                {
                    var replaceApostrophe = Regex.Replace(item, @"&#8217;", "'").Trim();
                    var replaceAmp = Regex.Replace(replaceApostrophe, @"&#038;", "&");
                    var replaceDash = Regex.Replace(replaceAmp, @"&#8211;", "-");
                    parsedBarNames.Add(replaceDash);
                }

                return parsedBarNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<string> ParseHappyHourDescription(IEnumerable<HtmlNode> happyHours)
        {
            try
            {
                var parsedDescriptions = new List<string>();

                foreach (var item in happyHours)
                {
                    item.RemoveChild(item.SelectSingleNode("a"));

                    var itemText = item.InnerText.Trim().Remove(0, 1); //removes initial semi-colon
                    var replaceLineBreaks = Regex.Replace(itemText, @"\t|\n|\r|", "").Trim();
                    parsedDescriptions.Add(replaceLineBreaks);
                }

                return parsedDescriptions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
