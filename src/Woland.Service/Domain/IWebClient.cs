// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWebClient.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IWebClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Service.Domain
{
    using System.Net.Http;

    /// <summary>
    /// Provides capabilities of interacting with the website.
    /// </summary>
    public interface IWebClient
    {
        /// <summary>
        /// Gets the web resource.
        /// </summary>
        /// <param name="url">
        /// The url to be retrieved.
        /// </param>
        /// <returns>
        /// The web resource content.
        /// </returns>
        string Get(string url);

        /// <summary>
        /// Posts the request to the web service.
        /// </summary>
        /// <param name="url">
        /// The url to be called.
        /// </param>
        /// <param name="content">
        /// The content of the message.
        /// </param>
        /// <returns>
        /// The result of the call.
        /// </returns>
        string Post(string url, HttpContent content);
    }
}