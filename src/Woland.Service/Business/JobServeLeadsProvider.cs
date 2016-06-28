namespace Woland.Service.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;

    using HtmlAgilityPack;

    using Domain;
    using Domain.Entities;

    public class JobServeLeadsProvider : ILeadsProvider
    {
        private const string JobServeAddress = "http://www.jobserve.com/";

        private readonly IWebClient webClient;

        public JobServeLeadsProvider(IWebClient webClient)
        {
            this.webClient = webClient;
        }

        public IEnumerable<JobLead> GetLatestLeads(string keyword, string location)
        {
            using (var client = new HttpClient())
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

                var searchResult = this.webClient.Post($"{JobServeAddress}/gb/en/mob/jobsearch", new FormUrlEncodedContent(form));
                var searchResultPage = this.GetHtmlDocument(searchResult);

                var links = this.ExtractLinks(searchResultPage);

                var pages = links
                    .Where(x => Regex.IsMatch(x, "^/gb/en/mob/jobsearch/results/"))
                    .Distinct()
                    .Select(x => this.webClient.Get($"{JobServeAddress}{x}"))
                    .Select(this.GetHtmlDocument)
                    .Concat(new[] { searchResultPage })
                    .ToList();
                var jobLinks = pages
                    .SelectMany(this.ExtractLinks)
                    .Where(x => Regex.IsMatch(x, "^/gb/en/mob/job/"))
                    .Select(x => Regex.Replace(x, @"^(.*?)(\?.+)?$", "$1"))
                    .Take(3)
                    .Select(x => this.webClient.Get($"{JobServeAddress}{x}"))
                    .Select(this.GetHtmlDocument)
                    .Select(this.ConvertToLead)
                    .ToList();

                return null;
            }
        }

        private JobLead ConvertToLead(HtmlDocument jobPage)
        {
            return null;
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