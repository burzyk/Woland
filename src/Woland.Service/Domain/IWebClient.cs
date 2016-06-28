namespace Woland.Service.Domain
{
    using System.Net.Http;

    public interface IWebClient
    {
        string Get(string url);

        string Post(string url, HttpContent content);
    }
}