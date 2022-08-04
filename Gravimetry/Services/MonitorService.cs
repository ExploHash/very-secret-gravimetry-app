
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
    public class MonitorService
    {
        readonly ApiClient _apiClient = new ApiClient();

        public MonitorService()
        {
        }

        public async Task<SiteMonitor> GetMonitor(int monitorId)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response;
            try
            {
                response = await _apiClient.client.GetAsync("/Monitor/" + monitorId);

                if (response.IsSuccessStatusCode)
                {
                    //Read content as normal string
                    var content = await response.Content.ReadAsStringAsync();
                    //Because the response are teams in json format, convert those to actual team objects
                    return JsonConvert.DeserializeObject<SiteMonitor>(content);
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
