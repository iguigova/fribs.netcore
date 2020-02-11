using ServiceStack;
using System;
using System.Collections.Specialized;

namespace fribs.netcore.api.clients.ss
{
    public interface IClient
    {
        NameValueCollection Headers { get; } // for session support

        void SetCredentials(string username, string password); // for BasicAuth support

        TResponse Post<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null);

        TResponse Put<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null);

        TResponse Patch<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null);

        TResponse Get<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null);

        TResponse Delete<TResponse>(IReturn<TResponse> request, Action<WebServiceException> onException = null);

        Action<Exception> OnException { get; set; }
    }
}
