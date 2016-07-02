﻿namespace Woland.Service.Business
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Entities;

    public class DefaultWebClient : IWebClient
    {
        private readonly IServiceLog log;

        private readonly ITimeProvider timeProvider;

        private readonly IDataContext context;

        public DefaultWebClient(IServiceLog log, ITimeProvider timeProvider, IDataContext context)
        {
            this.log = log;
            this.timeProvider = timeProvider;
            this.context = context;
        }

        public string Get(string url)
        {
            using (var client = this.CreateClient())
            {
                return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            }
        }

        public string Post(string url, HttpContent content)
        {
            using (var client = this.CreateClient())
            {
                return client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            }
        }

        private HttpClient CreateClient()
        {
            return new HttpClient(new LoggingMessageHandler(new HttpClientHandler(), this.log, this.timeProvider, this.context));
        }

        private class LoggingMessageHandler : DelegatingHandler
        {
            private readonly IServiceLog log;

            private readonly ITimeProvider timeProvider;

            private readonly IDataContext context;

            public LoggingMessageHandler(HttpMessageHandler innerHandler, IServiceLog log, ITimeProvider timeProvider, IDataContext context)
                : base(innerHandler)
            {
                this.log = log;
                this.timeProvider = timeProvider;
                this.context = context;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                this.log.Info("Sending request ...");
                var response = await base.SendAsync(request, cancellationToken);

                try
                {
                    this.log.Info("Saving the request details into the database...");
                    var logEntry = new WebRequestLog
                    {
                        Url = request.RequestUri.ToString(),
                        Method = request.Method.ToString(),
                        Timestamp = this.timeProvider.Now,
                        RequestBody = request.Content?.ReadAsStringAsync().Result,
                        Request = request.ToString(),
                        ResponseBody = response.Content?.ReadAsStringAsync().Result,
                        Response = response.ToString(),
                        ResponseCode = (int)response.StatusCode
                    };
                    this.context.WebRequestLogs.Add(logEntry);
                    this.context.SaveChanges();

                    this.log.Info("Request logged");
                }
                catch (Exception ex)
                {
                    this.log.Error("Unable to save the request: {0}", ex);
                }

                return response;
            }
        }
    }
}