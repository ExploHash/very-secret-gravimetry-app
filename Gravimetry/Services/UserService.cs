using System.Net.Http;
using Gravimetry.Models;
using Gravimetry.Clients;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Gravimetry.Services
{
    public class UserService
    {
        readonly ApiClient _apiClient = new ApiClient();

        public UserService()
        {
        }

        public virtual async Task<List<Team>> GetTeams()
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response = await _apiClient.client.GetAsync("/Users/Teams");

            if (response.IsSuccessStatusCode)
            {
                //Read content as normal string
                var content = await response.Content.ReadAsStringAsync();
                //Because the response are teams in json format, convert those to actual team objects
                return JsonConvert.DeserializeObject<List<Team>>(content);
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get teams.", "Cancel");
            }

            return null;
        }

        public async Task<List<SiteMonitor>> GetMonitors(string search)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response = await _apiClient.client.GetAsync("/Users/Monitors?search=" + search);

            if (response.IsSuccessStatusCode)
            {
                //Read content as normal string
                var content = await response.Content.ReadAsStringAsync();
                //Because the response are teams in json format, convert those to actual team objects
                return JsonConvert.DeserializeObject<List<SiteMonitor>>(content);
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get monitors.", "Cancel");
            }

            return null;
        }

        public async Task<List<Incident>> GetActiveIncidents()
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response = await _apiClient.client.GetAsync("/Users/ActiveIncidents");

            if (response.IsSuccessStatusCode)
            {
                //Read content as normal string
                var content = await response.Content.ReadAsStringAsync();
                //Because the response are teams in json format, convert those to actual team objects
                return JsonConvert.DeserializeObject<List<Incident>>(content);
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get incidents.", "Cancel");
            }

            return null;
        }

        public async Task<bool> JoinTeam(int id)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response = await _apiClient.client.PutAsync("/Users/Teams/" + id, null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to join team.", "Cancel");
            }

            return false;
        }

        public async Task<bool> LeaveTeam(int id)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response = await _apiClient.client.DeleteAsync("/Users/Teams/" + id);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to leave team.", "Cancel");
            }

            return false;
        }
    }
}
