namespace Woland.Business
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using Domain;
    using Domain.Entities;
    using HtmlAgilityPack;

    public class JobServeLeadsProvider : ILeadsProvider
    {
        private const string ProviderName = "JobServe";

        private const string JobServeAddress = "http://www.jobserve.com/";

        private readonly IWebClient webClient;

        private readonly IServiceLog log;

        public JobServeLeadsProvider(IWebClient webClient, IServiceLog log)
        {
            this.webClient = webClient;
            this.log = log;
        }

        public IEnumerable<JobLead> GetLatestLeads(string keyword, string location, int index, int count)
        {
            var form = new[]
            {
                new KeyValuePair<string, string>("JobSearch.Keywords", keyword),
                new KeyValuePair<string, string>("JobSearch.Location", location),
                new KeyValuePair<string, string>("JobSearch.LocationID", string.Empty),
                new KeyValuePair<string, string>("JobSearch.RoleType", "CONTRACT"),
                new KeyValuePair<string, string>("JobSearch.JobAge", "7"),
                new KeyValuePair<string, string>("JobSearch.SortBy", "EXPLORER_DATE_DESC"),
                new KeyValuePair<string, string>("JobSearch.SearchMode", "QuickSearch"),
                new KeyValuePair<string, string>("ChangeMode", "false"),
            };

            this.log.Info($"Getting Job leads from JobServe: keyword: {keyword}, location: {location}, index: {index}, count: {count}");
            var searchResult = this.webClient.Post($"{JobServeAddress}/gb/en/mob/jobsearch", new FormUrlEncodedContent(form));
            var searchResultPage = this.GetHtmlDocument(searchResult);

            var links = this.ExtractLinks(searchResultPage);

            var otherPages = links
                .Where(x => Regex.IsMatch(x, "^/gb/en/mob/jobsearch/results/"))
                .Distinct()
                .Select(x => this.webClient.Get($"{JobServeAddress}{x}"))
                .Select(this.GetHtmlDocument)
                .ToList();
            var allPages = new[] { searchResultPage }.Concat(otherPages);
            var leads = allPages
                .SelectMany(this.ExtractLinks)
                .Where(x => Regex.IsMatch(x, "^/gb/en/mob/job/"))
                .Select(x => Regex.Replace(x, @"^(.*?)(\?.+)?$", "$1"))
                .Skip(index)
                .Take(count)
                .Select(x => this.webClient.Get($"{JobServeAddress}{x}"))
                .Select(this.GetHtmlDocument)
                .Select(this.ConvertToLead)
                .ToList();

            this.log.Info($"Found: {leads.Count} leads");

            foreach (var lead in leads)
            {
                lead.SearchDetails = new SearchDetails
                {
                    Keywords = keyword,
                    Location = location
                };
                lead.SourceName = ProviderName;
            }

            return leads;
        }

        private JobLead ConvertToLead(HtmlDocument jobPage)
        {
            DateTime timestamp;
            var title = jobPage.DocumentNode
                 .Descendants("article")
                 .Where(x => x.GetAttributeValue("class", string.Empty) == "job")
                 .SelectMany(x => x.Descendants("header"))
                 .Select(x => x.Descendants("h3").FirstOrDefault())
                 .Where(x => x != null)
                 .Select(x => x.WriteContentTo())
                 .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
            var rates = this.ExtractRates(this.GetValueByLabel(jobPage, "Rate"));
            rates = !rates.Any() ? this.ExtractRates(title) : rates;
            var isDaily = rates.Any(x => x < 200);

            return new JobLead
            {
                Title = title,
                Body = jobPage.DocumentNode
                    .Descendants("div")
                    .Where(x => x.GetAttributeValue("class", string.Empty) == "jobdesc")
                    .Select(x => x.WriteContentTo().Trim())
                    .FirstOrDefault(),
                AgencyName = this.GetValueByLabel(jobPage, "Employment Business", "Company"),
                FullName = this.GetValueByLabel(jobPage, "Contact"),
                PostedTimestamp = DateTime.TryParseExact(
                    this.GetValueByLabel(jobPage, "Posted Date"),
                    "dd/MM/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out timestamp) ? timestamp : null as DateTime?,
                Telephone = this.GetSiblingsByLabel(jobPage, "Telephone")
                    .Select(x => x.NextSibling?.InnerText)
                    .SingleOrDefault(),
                SourceUrl = this.GetSiblingsByLabel(jobPage, "Permalink")
                    .Select(x => x.NextSibling?.GetAttributeValue("href", string.Empty))
                   .SingleOrDefault(),
                MinRate = rates.Any() ? (isDaily ? rates.First() * 8 : rates.First()) : null as int?,
                MaxRate = rates.Any() ? (isDaily ? rates.Last() * 8 : rates.Last()) : null as int?
            };
        }

        private IList<int> ExtractRates(string value)
        {
            return Regex.Matches(HtmlEntity.DeEntitize(value ?? string.Empty), @"£(\d+)")
                .Cast<Match>()
                .Where(x => x.Success)
                .Select(x => int.Parse(x.Groups[1].Value))
                .Where(x => x != 0)
                .ToList();
        }

        private IList<HtmlNode> GetSiblingsByLabel(HtmlDocument jobPage, params string[] values)
        {
            return jobPage.DocumentNode
                .Descendants("label")
                .Where(x => values.Contains(x.WriteContentTo().Trim()))
                .Select(x => x.NextSibling)
                .ToList();
        }

        private string GetValueByLabel(HtmlDocument jobPage, params string[] values)
        {
            return this.GetSiblingsByLabel(jobPage, values)
                .Select(x => (x.InnerText ?? string.Empty).Trim())
                .SingleOrDefault();
        }

        private HtmlDocument GetHtmlDocument(string html)
        {
            var result = new HtmlDocument();
            result.LoadHtml(html);

            return result;
        }

        private IList<string> ExtractLinks(HtmlDocument document)
        {
            return document.DocumentNode.Descendants("a")
                .Select(x => x.Attributes.Contains("href") ? x.Attributes["href"].Value : null)
                .Where(x => x != null)
                .ToList();
        }
    }
}