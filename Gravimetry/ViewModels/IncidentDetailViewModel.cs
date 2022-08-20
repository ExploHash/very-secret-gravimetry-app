using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gravimetry.Models;
using Gravimetry.Services;
using Gravimetry.Classes;
using System.Collections.Generic;
using System.Web;

namespace Gravimetry.ViewModels
{
    public class IncidentDetailViewModel: BaseViewModel, IQueryAttributable
    {
        readonly IncidentService _incidentService = new IncidentService();

        public Command SendMessage { get; }

        public IncidentDetailViewModel()
        {
            SendMessage = new Command(async () => await OnSendMessage());

            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                // If you want to update UI, make sure its on the on the main thread.
                // Otherwise, you can remove the BeginInvokeOnMainThread
                Device.BeginInvokeOnMainThread(async () => await LoadIncident(IncidentId));
                return IsRefreshing;
            });
        }



        public bool IsRefreshing = true;

        private string _message;

        public string message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        private Incident _incident;

        public Incident incident
        {
            get { return _incident; }
            set
            {
                _incident = value;
                OnPropertyChanged();
            }
        }

        int _incidentId;

        public int IncidentId
        {
            get
            {
                return _incidentId;
            }
            set
            {
                _incidentId = value;
                LoadIncident(value);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            IncidentId = int.Parse(HttpUtility.UrlDecode(query["IncidentId"]));
        }

        public async Task LoadIncident(int incidentId)
        {
            try
            {
               incident = await _incidentService.GetIncident(incidentId); //grab from api
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task OnSendMessage()
        {
            await _incidentService.PostMessage(IncidentId, message);
            message = "";
        }
    }
}
