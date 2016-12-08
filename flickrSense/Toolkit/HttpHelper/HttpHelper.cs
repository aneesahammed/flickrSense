﻿/*
 * @file:HttpHelper
 * @brief: This class exposes functionality of HttpClient through a singleton to take advantage of built-in connection pooling.
 * @credit:https://github.com/Microsoft/UWPCommunityToolkit 
 */

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace Microsoft.Toolkit.Uwp
{
    /// <summary>
    /// This class exposes functionality of HttpClient through a singleton to take advantage of built-in connection pooling.
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// Maximum number of Http Clients that can be pooled.
        /// </summary>
        private const int DefaultPoolSize = 10;

        /// <summary>
        /// Private singleton field.
        /// </summary>
        private static HttpHelper _instance;

        private SemaphoreSlim _semaphore = null;

        /// <summary>
        /// Private instance field.
        /// </summary>
        private ConcurrentQueue<HttpClient> _httpClientQueue = null;

        private IHttpFilter _httpFilter = null;

        /// <summary>
        /// Gets public singleton property.
        /// </summary>
        public static HttpHelper Instance => _instance ?? (_instance = new HttpHelper());

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHelper"/> class.
        /// </summary>
        public HttpHelper()
            : this(DefaultPoolSize, GetDefaultFilter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHelper"/> class.
        /// </summary>
        /// <param name="poolSize">number of HttpClient instances allowed</param>
        /// <param name="httpFilter">HttpFilter to use when instances of HttpClient are created</param>
        public HttpHelper(int poolSize, IHttpFilter httpFilter)
        {
            _httpFilter = httpFilter;
            _semaphore = new SemaphoreSlim(poolSize);
            _httpClientQueue = new ConcurrentQueue<HttpClient>();
        }

        /// <summary>
        /// Process Http Request using instance of HttpClient.
        /// </summary>
        /// <param name="request">instance of <see cref="HttpHelperRequest"/></param>
        /// <returns>Instane of <see cref="HttpHelperResponse"/></returns>
        public async Task<HttpHelperResponse> SendRequestAsync(HttpHelperRequest request)
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);

            HttpClient client = null;

            try
            {
                var httpRequestMessage = request.ToHttpRequestMessage();

                client = GetHttpClientInstance();

                var response = await client.SendRequestAsync(httpRequestMessage).AsTask().ConfigureAwait(false);

                FixInvalidCharset(response);

                return new HttpHelperResponse(response);
            }
            finally
            {
                // Add the HttpClient instance back to the queue.
                if (client != null)
                {
                    _httpClientQueue.Enqueue(client);
                }

                _semaphore.Release();
            }
        }

        private static IHttpFilter GetDefaultFilter()
        {
            var filter = new HttpBaseProtocolFilter();
            filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            return filter;
        }

        private HttpClient GetHttpClientInstance()
        {
            HttpClient client = null;

            // Try and get HttpClient from the queue
            if (!_httpClientQueue.TryDequeue(out client))
            {
                

                client = new HttpClient(_httpFilter);
            }

            return client;
        }

        /// <summary>
        /// Fix invalid charset returned by some web sites.
        /// </summary>
        /// <param name="response">HttpResponseMessage instance.</param>
        private void FixInvalidCharset(HttpResponseMessage response)
        {
            if (response != null && response.Content != null && response.Content.Headers != null
                && response.Content.Headers.ContentType != null && response.Content.Headers.ContentType.CharSet != null)
            {
                // Fix invalid charset returned by some web sites.
                string charset = response.Content.Headers.ContentType.CharSet;
                if (charset.Contains("\""))
                {
                    response.Content.Headers.ContentType.CharSet = charset.Replace("\"", string.Empty);
                }
            }
        }
    }
}
