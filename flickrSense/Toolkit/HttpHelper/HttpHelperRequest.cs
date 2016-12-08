/*
 * @file:HttpHelperRequest
 * @brief: Represents an HTTP request message including headers.
 * @credit:https://github.com/Microsoft/UWPCommunityToolkit 
 */

using System;
using System.Collections.Generic;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace Microsoft.Toolkit.Uwp
{
    /// <summary>
    /// Represents an HTTP request message including headers.
    /// </summary>
    public class HttpHelperRequest : IDisposable
    {
        private HttpRequestMessage _requestMessage = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHelperRequest"/> class with Uri and HTTP GET Method.
        /// </summary>
        /// <param name="uri">Uri for the resource</param>
        public HttpHelperRequest(Uri uri)
            : this(uri, HttpMethod.Get)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHelperRequest"/> class with Uri and HTTP method.
        /// </summary>
        /// <param name="uri">Uri for the resource</param>
        /// <param name="method">Method to use when making the request</param>
        public HttpHelperRequest(Uri uri, HttpMethod method)
        {
            _requestMessage = new HttpRequestMessage(method, uri);
        }

        /// <summary>
        /// Gets the http reqeust method.
        /// </summary>
        public HttpMethod Method
        {
            get { return _requestMessage.Method; }
        }

        /// <summary>
        /// Gets the request Uri.
        /// </summary>
        public Uri RequestUri
        {
            get { return _requestMessage.RequestUri; }
        }

        /// <summary>
        /// Gets HTTP header collection from the underlying HttpRequestMessage.
        /// </summary>
        public HttpRequestHeaderCollection Headers
        {
            get
            {
                return _requestMessage.Headers;
            }
        }

        /// <summary>
        /// Gets or sets holds request Result.
        /// </summary>
        public IHttpContent Content
        {
            get
            {
                return _requestMessage.Content;
            }

            set
            {
                _requestMessage.Content = value;
            }
        }

        /// <summary>
        /// Creates HttpRequestMessage using the data.
        /// </summary>
        /// <returns>Instance of <see cref="HttpRequestMessage"/></returns>
        public HttpRequestMessage ToHttpRequestMessage()
        {
            return _requestMessage;
        }

        /// <summary>
        /// Dispose underlying content
        /// </summary>
        public void Dispose()
        {
            try
            {
                _requestMessage.Dispose();
            }
            catch (ObjectDisposedException)
            {
                // known issue
                // http://stackoverflow.com/questions/39109060/httpmultipartformdatacontent-dispose-throws-objectdisposedexception
            }
        }
    }
}
