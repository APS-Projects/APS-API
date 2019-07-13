using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TriviaDrunksScraper.HappyHours.HappyHourDAOs;

namespace TriviaDrunksScraper.HappyHours
{
    public class HappyHourHtmlParsers : IHappyHourHtmlParsers
    {
        private ISiteDirectory _siteDirectory;
        private HttpClient _httpClient;
        private IConfiguration _config;

        private string conn { get => _config.GetConnectionString("ConnectionString"); }

        public HappyHourHtmlParsers(ISiteDirectory siteDirectory, HttpClient httpClient, IConfiguration config)
        {
            _siteDirectory = siteDirectory;
            _httpClient = httpClient;
            _config = config;
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

                var parsedBarNames = ParseBarNames(happyHours);

                var happyHourCopy = new List<HtmlNode>(happyHours);

                var parsedDescriptions = ParseHappyHourDescription(happyHourCopy);

                UpdateBars(parsedBarNames);

                //Build a list of reference types with correct properties and pass it to dapper like below
                //string processQuery = "INSERT INTO PROCESS_LOGS VALUES (@A, @B)";
                //connection.Execute(processQuery, processList);

                return happyHours;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateBars(IEnumerable<string> barNames)
        {
            try
            {
                IEnumerable<BarDAO> barsToUpdate = barNames.Select(bar => new BarDAO { Name = bar, CityId = 1 });

                using (IDbConnection db = new SqlConnection(conn))
                {
                    var updateBars = @"MERGE Bar As Target
                                   USING @barsToUpdate As Source
                                   ON Target.Name = Source.Name AND Bar.CityId = Source.CityId
                                   WHEN MATCHED
                                        THEN ";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

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
