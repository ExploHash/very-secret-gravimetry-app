using System.Net.Http;
using Gravimetry.Models;
using Gravimetry.Clients;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Gravimetry.Services
{
    public class TeamsService
    {
        readonly ApiClient _apiClient = new ApiClient();

        public TeamsService()
        {
        }

        public virtual async Task<List<Team>> GetPublicTeams()
        {
            //Call backend based on custom apiclient class

            HttpResponseMessage response = await _apiClient.client.GetAsync("/Teams");

            if (response.IsSuccessStatusCode)
            {
                //Read content as normal string
                var content = await response.Content.ReadAsStringAsync();
                //Because the response are teams in json format, convert those to actual team objects
                return JsonConvert.DeserializeObject<List<Team>>(content);
            }
            else
            {
                await Shell.Current.DisplayAlert("ApiError", "Failed to get public teams.", "Cancel");
            }


            return null;
        }
    }
}
