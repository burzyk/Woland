namespace Woland.Service.Business
{
    using System.Net.Http;

    using Domain;

    public class DefaultWebClient : IWebClient
    {
        public string Get(string url)
        {
            using (var client = new HttpClient())
            {
                return client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            }
        }

        public string Post(string url, HttpContent content)
        {
            using (var client = new HttpClient())
            {
                return client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
            }
        }
    }
}