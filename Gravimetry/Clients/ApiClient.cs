using System;
using System.Net.Http;
using System.Net;
using Gravimetry.Classes;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Gravimetry.Clients
{
    public class ApiClient
    {
        public HttpClientHandler httpClientHandler = new HttpClientHandler();
        public CookieContainer cookies = new CookieContainer();

        public ApiClient()
        {
            //Make sure httpclient ignores invalid certicates
            this.httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            this.httpClientHandler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            //Add cookiejar
            this.httpClientHandler.CookieContainer = this.cookies;

        }

        public HttpClient client
        {
            get
            {
                //Add token to cookiejar if exists
                var accessToken = Preferences.Get("accessToken", "");

                if (accessToken.Length > 0)
                {
                    this.cookies.Add(new Cookie(".AspNetCore.Identity.Application", accessToken));
                }

                //Initialize httpclient based on handler
                var client = new HttpClient(this.httpClientHandler);

                //Set baseaddress to api adress
                client.BaseAddress = new Uri("https://10.0.2.2:5001");

                return client;
            }
        }
    }
}
