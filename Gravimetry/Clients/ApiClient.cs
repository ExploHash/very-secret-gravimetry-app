using System;
using System.Net.Http;
using System.Net;
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

        //Getter function so everytime a call is run this is reinitialized
        //That way we always grab the latest accesstoken from preferences
        public HttpClient client
        {
            get
            {
                //Add token to cookiejar if exists
                var accessToken = Preferences.Get("accessToken", "");
                var host = DeviceInfo.Platform == DevicePlatform.Android ? "10.0.2.2" : "127.0.0.1";
                if (accessToken.Length > 0)
                {
                    this.cookies.Add(new Cookie(".AspNetCore.Identity.Application", accessToken, "/", host));
                }

                //Initialize httpclient based on handler
                var client = new HttpClient(this.httpClientHandler);

                //Set baseaddress to api adress based on platform
                client.BaseAddress = new Uri($"https://{host}:5001");

                //return client
                return client;
            }
        }
    }
}
