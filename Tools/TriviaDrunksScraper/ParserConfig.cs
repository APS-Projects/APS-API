using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TriviaDrunksScraper.HappyHours;
using TriviaDrunksScraper.Repositories;

namespace TriviaDrunksScraper
{
    public class ParserConfig
    {
        public ISiteDirectory SiteDirectory { get; set; }
        public HttpClient HttpClient { get; set; }
        public TriviaDrunkRepository Repository { get; set; }
        public ParserConfig(ISiteDirectory siteDirectory, HttpClient httpClient, TriviaDrunkRepository repository)
        {
            SiteDirectory = siteDirectory;
            HttpClient = httpClient;
            Repository = repository;
        }
    }
}
