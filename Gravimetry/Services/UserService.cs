
using System.Net.Http;
using System.Net;
using System;
using Gravimetry.Classes;
using Gravimetry.Models;
using Gravimetry.Clients;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gravimetry.Services
{
    public class UserService
    {
        readonly ApiClient _apiClient = new ApiClient();

        public UserService()
        {
        }

        public async Task<List<Team>> GetTeams()
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response;
            try
            {
                response = await _apiClient.client.GetAsync("/Users/Teams");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Team>>(content);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Fuck");
            }

            //Check if successfull
            

            return null;
        }
    }
}
