using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TriviaDrunksScraper.HappyHours
{
    public interface IHappyHourHtmlParsers
    {
        Task<IEnumerable<HtmlNode>> GetHtmlNashville();
    }
}