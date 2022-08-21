
using System.Net.Http;
using System.Net;
using System;
using Gravimetry.Classes;
using Gravimetry.Clients;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gravimetry.Services
{
    public class AuthService
    {
        readonly ApiClient _apiClient = new ApiClient();

        public AuthService()
        {
        }

        public async Task<bool> SignIn(string username, string password)
        {

            //Create input object based on arguments
            var loginInput = new LoginInput();
            loginInput.Name = username;
            loginInput.Password = password;

            //Call backend based on custom apiclient class
            var response = await _apiClient.client.PostAsJsonAsync("/Users/Login", loginInput);

            //Check if successfull
            if (response.IsSuccessStatusCode)
            {
                //Grab cookies based on request url
                var seperateCookies = _apiClient.cookies.GetCookies(response.RequestMessage.RequestUri);

                //Loop through cookies and save token for next request
                foreach (Cookie cookie in seperateCookies)
                {
                    if (cookie.Name == ".AspNetCore.Identity.Application")
                    {
                        Preferences.Set("accessToken", cookie.Value);
                    }
                }

                return true;
            }

            return false;
        }
    }
}
