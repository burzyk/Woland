﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobServeLeadsProvider.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the JobServeLeadsProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Service.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;

    using HtmlAgilityPack;

    using Woland.Service.Domain;
    using Woland.Service.Domain.Entities;

    /// <summary>
    /// JobServe implementation of the <see cref="ILeadsProvider"/> interface.
    /// </summary>
    public class JobServeLeadsProvider : ILeadsProvider
    {
        /// <summary>
        /// Gets the latest <see cref="JobLead"/> form JobServe.
        /// </summary>
        /// <param name="keyword">
        ///     The keyword that is used to search for jobs.
        /// </param>
        /// <param name="location">
        ///     The geographical location of the lead.
        /// </param>
        /// <returns>
        /// The list of leads.
        /// </returns>
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

                var searchResult =
                    client.PostAsync("http://www.jobserve.com/gb/en/mob/jobsearch", new FormUrlEncodedContent(form))
                        .Result.Content.ReadAsStringAsync()
                        .Result;

                var searchResultPage = new HtmlDocument();
                searchResultPage.LoadHtml(searchResult);
                var links =
                    searchResultPage.DocumentNode.Descendants("a")
                        .Select(x => x.Attributes.Contains("href") ? x.Attributes["href"].Value : null)
                        .Where(x => x != null)
                        .ToList();
                var pages = links.Where(x => Regex.IsMatch(x, "^/gb/en/mob/jobsearch/results/")).Distinct().ToList();
                var jobLinks = links.Where(x => Regex.IsMatch(x, "^/gb/en/mob/job/")).ToList();

                return null;
            }
        }
    }
}