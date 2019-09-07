using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TriviaDrunksScraper.HappyHours
{
    public interface IHtmlParser
    {
        Task<IEnumerable<HtmlNode>> ParseHappyHourHtml();

        //Task<IEnumerable<HtmlNode>> ParseTriviaHtml();
    }
}