
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
    public class ManagerService
    {
        readonly ApiClient _apiClient = new ApiClient();

        public ManagerService()
        {
        }

        public async Task<Team> Create(TeamInput teamInput)
        {
            //Call backend based on custom apiclient class
            var response = await _apiClient.client.PostAsJsonAsync("/Teams", teamInput);

            //Check if successfull
            if (response.IsSuccessStatusCode)
            {
                //Read content as normal string
                var content = await response.Content.ReadAsStringAsync();
                //Because the response are teams in json format, convert those to actual team objects
                return JsonConvert.DeserializeObject<Team>(content);
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to create team.", "Cancel");
            }

            return null;
        }

        public async Task<Team> Update(int Id, string Name, bool IsPublic)
        {
            //Create input object based on arguments
            var input = new TeamInput();
            input.Name = Name;
            input.IsPublic = IsPublic;

            //Call backend based on custom apiclient class
            var response = await _apiClient.client.PutAsJsonAsync("/Teams/" + Id, input);

            if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to update team.", "Cancel");
            }

            return null;
        }

        public async Task Delete(int Id)
        {
            var response = await _apiClient.client.DeleteAsync("/Teams/" + Id);

            //Check if successfull
            if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to delete team.", "Cancel");
            }
        }

        public async Task<List<Team>> GetAllTeams()
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response = await _apiClient.client.GetAsync("/Teams/All");

            if (response.IsSuccessStatusCode)
            {
                //Read content as normal string
                var content = await response.Content.ReadAsStringAsync();
                //Because the response are teams in json format, convert those to actual team objects
                return JsonConvert.DeserializeObject<List<Team>>(content);
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get all teams.", "Cancel");
            }

            return null;
        }

        public async Task<List<SiteMonitor>> GetAllMonitors()
        {

            HttpResponseMessage response = await _apiClient.client.GetAsync("/Monitor");

            if (response.IsSuccessStatusCode)
            {
                //Read content as normal string
                var content = await response.Content.ReadAsStringAsync();
                //Because the response are teams in json format, convert those to actual team objects
                return JsonConvert.DeserializeObject<List<SiteMonitor>>(content);
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get all monitors.", "Cancel");
            }

            //Check if successfull


            return null;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response = await _apiClient.client.GetAsync("/Users");

            if (response.IsSuccessStatusCode)
            {
                //Read content as normal string
                var content = await response.Content.ReadAsStringAsync();
                //Because the response are teams in json format, convert those to actual team objects
                return JsonConvert.DeserializeObject<List<ApplicationUser>>(content);
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get all user.", "Cancel");
            }

            return null;
        }

        public async Task<bool> AddUser(int teamId, string userId)
        {
            //Call backend based on custom apiclient class
            HttpResponseMessage response = await _apiClient.client.PutAsync($"/Teams/{teamId}/User/" + userId, null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get add user to team.", "Cancel");
            }

            return false;
        }

        public async Task<bool> DeleteUser(int teamId, string userId)
        {
            //Call backend based on custom apiclient class

            HttpResponseMessage response = await _apiClient.client.DeleteAsync($"/Teams/{teamId}/User/" + userId);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get delete user from team.", "Cancel");
            }

            return false;
        }

        public async Task<bool> AddMonitor(int teamId, int monitorId)
        {
            HttpResponseMessage response = await _apiClient.client.PutAsync($"/Teams/{teamId}/Monitor/" + monitorId, null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get add monitor to team.", "Cancel");
            }

            return false;
        }

        public async Task<bool> DeleteMonitor(int teamId, int monitorId)
        {
            //Call backend based on custom apiclient class

            HttpResponseMessage response = await _apiClient.client.DeleteAsync($"/Teams/{teamId}/Monitor/" + monitorId);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get delete monitor from team.", "Cancel");
            }

            return false;
        }

        public async Task<Team> GetTeam(int id)
        {

            HttpResponseMessage response = await _apiClient.client.GetAsync("/Teams/" + id);

            if (response.IsSuccessStatusCode)
            {
                //Read content as normal string
                var content = await response.Content.ReadAsStringAsync();
                //Because the response are teams in json format, convert those to actual team objects
                return JsonConvert.DeserializeObject<Team>(content);
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get team.", "Cancel");
            }

            return null;
        }

    }
}
