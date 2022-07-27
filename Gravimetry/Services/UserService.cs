
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

        public async Task<bool> JoinTeam(int id)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response;
            try
            {
                response = await _apiClient.client.PutAsync("/Users/Teams/" + id, null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fuck");
            }

            return false;
        }

        public async Task<bool> LeaveTeam(int id)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response;
            try
            {
                response = await _apiClient.client.DeleteAsync("/Users/Teams/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fuck");
            }

            return false;
        }
    }
}
