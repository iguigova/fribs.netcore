using System;
using System.Collections.Specialized;
using System.Net;
using System.Text.RegularExpressions;
using ServiceStack;

namespace fribs.netcore.api.clients.ss
{
    public class JsonHttpClient : IClient
    {
        protected ServiceStack.JsonHttpClient _client;

        public ServiceStack.JsonHttpClient Client { get { return _client; } }

        public JsonHttpClient(string baseUrl, Action<Exception> onException = null)
        {
            _client = new ServiceStack.JsonHttpClient(baseUrl);

            OnException = onException;
        }

        public void SetCredentials(string username, string password) 
        {
            _client.SetCredentials(username, password);
        }

        public NameValueCollection Headers { get { return _client.Headers; } }

        public TResponse Post<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null)
        {
            return Log(() => { return Client.Post(request); }, request, onException);
        }

        public TResponse Put<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null)
        {
            return Log(() => { return Client.Put(request); }, request, onException);
        }

        public TResponse Patch<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null)
        {
            return Log(() => { return Client.Patch(request); }, request, onException);
        }

        public TResponse Get<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null)
        {
            return Log(() => { return Client.Get(request); }, request, onException);
        }

        public TResponse Delete<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null)
        {
            return Log(() => { return Client.Delete(request); }, request, onException);
        }

        public Action<Exception> OnException { get; set; }

        private TResponse Log<TResponse>(Func<TResponse> function, IReturn<TResponse> request, Action<WebServiceException> onException = null)
        {
            Client.Headers[HttpRequestHeader.ContentType.ToString()] = Client.Headers[HttpRequestHeader.ContentType.ToString()] ?? "application/json";
            Client.Headers[HttpRequestHeader.Accept.ToString()] = Client.Headers[HttpRequestHeader.Accept.ToString()] ?? "application/json";
            Client.Headers[HttpRequestHeader.UserAgent.ToString()] = Client.Headers[HttpRequestHeader.UserAgent.ToString()] ?? "LogonTests/1.1+ (https://logonlabs.com/)";

            if (string.IsNullOrEmpty(Client.Headers[HttpRequestHeader.ContentType.ToString()]))
            {
                Client.Headers.Remove(HttpRequestHeader.ContentType.ToString());
            }

            Client.ClearCookies();

            try
            {
                var response = function();

                return response;
            }
            catch (WebServiceException ex)
            {
                ex.Data.Add("request", RemovePasswordInformation(request.SerializeToString()));
                ex.Data.Add("response", ex.ResponseBody.SerializeToString());

                (onException ?? OnException)?.Invoke(ex);
            }

            return default;
        }

        private static Regex _passwordPattern = new Regex(@"(password)=([^&]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private string RemovePasswordInformation(string text)
        {
            return _passwordPattern.Replace(text, "$1=*****");
        }
    }


}
