
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
    public class TeamsService
    {
        readonly ApiClient _apiClient = new ApiClient();

        public TeamsService()
        {
        }

        public async Task<List<Team>> GetPublicTeams()
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response;
            try
            {
                response = await _apiClient.client.GetAsync("/Teams");

                if (response.IsSuccessStatusCode)
                {
                    //Read content as normal string
                    var content = await response.Content.ReadAsStringAsync();
                    //Because the response are teams in json format, convert those to actual team objects
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
