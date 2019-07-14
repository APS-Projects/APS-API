using Dapper;
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

        private string conn { get => _config.GetConnectionString("DefaultConnection"); }

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

                UpdateBars(parsedBarNames, 1);

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

        private async void UpdateBars(IEnumerable<string> barNames, int cityId)
        {
            try
            {
                var test = conn;
                var testing = _config.GetConnectionString("DefaultConnection");
                var moreTesting = _config.GetSection("ConnectionStrings").GetSection("DefaultConnection");
                using (IDbConnection db = new SqlConnection(conn))
                {
                    List<BarDAO> barsToUpdate = barNames.Select(bar => new BarDAO { Name = bar, CityId = cityId }).ToList();

                    var updateBars = @"MERGE Bar As Target
                                   USING (VALUES (@Name, @CityId)) As Source(Name, CityId))
                                   ON Target.Name = Source.Name AND Bar.CityId = Source.CityId
                                   WHEN NOT MATCHED BY TARGET
                                        THEN 
                                   INSERT INTO Bar (Name, CityId)
                                   VALUES (Source.Name, Source.CityId);";

                    var insertBars = @"INSERT INTO Bar (Name, CityId)
                                       VALUES
                                       (@Name, @CityId);";

                    //foreach (var bar in collection)
                    //{

                    //}

                    var success = await db.ExecuteAsync(insertBars, barsToUpdate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
