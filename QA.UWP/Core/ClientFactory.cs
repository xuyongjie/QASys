using Account.Client;
using QA.Client;
using System;
using System.Net.Http;

namespace QA.UWP.Core
{
    public static class ClientFactory
    {
        // HTTPS address is required by the WebAuthenticationBroker
        //public const string BaseAddress = "http://localhost:11281/";
        public const string BaseAddress = "http://xyjvd.cloudapp.net";

        public static HttpClient CreateHttpClient()
        {
            AppSettings settings = new AppSettings();
            AccessTokenProvider loginProvider = new AccessTokenProvider();
            OAuth2BearerTokenHandler oauth2Handler = new OAuth2BearerTokenHandler(settings, loginProvider);
            HttpClient httpClient = HttpClientFactory.Create(oauth2Handler);
            httpClient.BaseAddress = new Uri(BaseAddress);
            httpClient.Timeout = TimeSpan.FromMinutes(2);
            return httpClient;
        }

        public static AccountClient CreateAccountClient()
        {
            return new AccountClient(CreateHttpClient());
        }

        public static QAClient CreateTravelClient()
        {
            return new QAClient(CreateHttpClient());
        }
       
    }
}
