/*
 * @file:HttpHelperResponse
 * @brief: HttpHelperResponse instance to hold data from Http Response.
 * @credit:https://github.com/Microsoft/UWPCommunityToolkit 
 */

using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace Microsoft.Toolkit.Uwp
{
    /// <summary>
    /// HttpHelperResponse instance to hold data from Http Response.
    /// </summary>
    public class HttpHelperResponse : IDisposable
    {
        private HttpResponseMessage _responseMessage = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHelperResponse"/> class.
        /// </summary>
        /// <param name="response">Http Response <see cref="HttpResponseMessage"/> being wrapped.</param>
        public HttpHelperResponse(HttpResponseMessage response)
        {
            _responseMessage = response;
        }

        /// <summary>
        /// Gets the HTTP response StatusCode.
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get
            {
                return _responseMessage.StatusCode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the HTTP response was successful.
        /// </summary>
        public bool Success
        {
            get
            {
                return _responseMessage.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Gets the HTTP response header collection.
        /// </summary>
        public HttpResponseHeaderCollection Headers
        {
            get
            {
                return _responseMessage.Headers;
            }
        }

        /// <summary>
        /// Gets content from HTTP response.
        /// </summary>
        public IHttpContent Content
        {
            get
            {
                return _responseMessage.Content;
            }
        }

        /// <summary>
        /// Reads the Content as string and returns it to the caller.
        /// </summary>
        /// <returns>string content</returns>
        public Task<string> GetTextResultAsync()
        {
            if (this.Content == null)
            {
                return Task.FromResult<string>(null);
            }

            return Content.ReadAsStringAsync().AsTask();
        }

        /// <summary>
        /// Dispose underlying content
        /// </summary>
        public void Dispose()
        {
            try
            {
                _responseMessage.Dispose();
            }
            catch (ObjectDisposedException)
            {
                // known issue
                // http://stackoverflow.com/questions/39109060/httpmultipartformdatacontent-dispose-throws-objectdisposedexception
            }
        }
    }
}
