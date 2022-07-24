
using System.Net.Http;
using System.Net;
using System;
using Gravimetry.Classes;
using Gravimetry.Clients;
using System.Threading.Tasks;
using Xamarin.Essentials;

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


        //public async Task<List<User>> GetUsers()
        //{
        //    try
        //    {
        //        //Build endpoint string 
        //        var uri = new Uri($"{GeneralHelpers.url}{GeneralHelpers.endpointU}");
        //        //Connect
        //        var response = await httpClient.GetAsync(uri);
        //        //If the response is successfull the content will be returned as a deserialized (JSON) list
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            return JsonConvert.DeserializeObject<List<User>>(content);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //When there is an exeption this message is posted with the stacktrace
        //        Console.WriteLine($"Exception occured in GetOfficePresences: {ex.Message}\n{ex.StackTrace}");
        //    }
        //    return null;
        //}
        //public async Task<User> PostUserAsync(User user)
        //{

        //    //Build endpoint string 
        //    var uri = new Uri($"{GeneralHelpers.url}{GeneralHelpers.endpointU}");
        //    //Make a JSON from the user object
        //    var json = JsonConvert.SerializeObject(user);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    //Post de user to the api
        //    var response = await httpClient.PostAsync(uri, content);
        //    //If the post is not successfull a pop-up appears with an error
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        _ = Application.Current.MainPage.DisplayAlert(AppRes.Alert, AppRes.AccountFailed, AppRes.Cancel);
        //    }
        //    //If it was successfull the response is transformed again, a pop-up for success appears and the content is returned
        //    var responseJson = await response.Content.ReadAsStringAsync();
        //    var responseContent = JsonConvert.DeserializeObject<User>(responseJson);
        //    _ = Application.Current.MainPage.DisplayAlert(Resources.AppRes.Success, Resources.AppRes.AccountCreated, Resources.AppRes.Ok);
        //    return responseContent;
        //}
    }
}
