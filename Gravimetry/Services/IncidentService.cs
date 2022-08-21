
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
using Xamarin.Forms;

namespace Gravimetry.Services
{
    public class IncidentService
    {
        readonly ApiClient _apiClient = new ApiClient();

        public IncidentService()
        {
        }

        public async Task<Incident> GetIncident(int incidentId)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response;
                response = await _apiClient.client.GetAsync("/Incident/" + incidentId);

                if (response.IsSuccessStatusCode)
                {
                    //Read content as normal string
                    var content = await response.Content.ReadAsStringAsync();
                    //Because the response are teams in json format, convert those to actual team objects
                    return JsonConvert.DeserializeObject<Incident>(content);
                }
                else
                {
                    await Shell.Current.DisplayAlert("ApiError", "Failed to get incident.", "Cancel");
                }

            return null;
        }

        public async Task PostMessage(int incidentId, string message)
        {
            //Create input object based on arguments
            var input = new IncidentNoteInput();
            input.Message = message;

            //Call backend based on custom apiclient class
            var response = await _apiClient.client.PostAsJsonAsync($"/Incident/{incidentId}/message", input);

            //Check if successfull
            if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to send message.", "Cancel");
            }
        }
    }
}
