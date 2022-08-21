using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Gravimetry.Models;
using Gravimetry.Views;
using Gravimetry.Views.Manager;
using Gravimetry.Services;
using System.ComponentModel;

namespace Gravimetry.ViewModels
{
    public class ActiveIncidentsViewModel : BaseViewModel //Needs to be based of BaseViewModel for stupid notify event handling
    {
        //Observable collection for items which are shown in view
        public ObservableCollection<Incident> Items { get; }
        //Command called by refreshview when refreshing
        public Command LoadItemsCommand { get; }

        public Command<Incident> ItemTapped { get; }

        public Command GoToManagerTeamsPage { get; }

        readonly UserService _userService = new UserService();


        public ActiveIncidentsViewModel()
        {
            //Initialize observer
            Items = new ObservableCollection<Incident>();
            GoToManagerTeamsPage = new Command(async () => await OnGoToManagerTeamsPage());
            //Initialize command and link to the method it actually executes when called
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Incident>(async (incident) => await OnItemTapped(incident));
        }

        public bool IsRefreshing = true;

        public void OnAppearing()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                // If you want to update UI, make sure its on the on the main thread.
                // Otherwise, you can remove the BeginInvokeOnMainThread
                Device.BeginInvokeOnMainThread(async () => await ExecuteLoadItemsCommand());
                return IsRefreshing;
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                var items = await _userService.GetActiveIncidents(); //grab from api
                Items.Clear(); //Clear list first
                foreach (var item in items) //Loop through and set them in the observablecollection
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task OnItemTapped(Incident incident)
        {
            await Shell.Current.GoToAsync($"{nameof(IncidentDetailPage)}?IncidentId={incident.Id}");
        }

        private async Task OnGoToManagerTeamsPage()
        {
            await Shell.Current.GoToAsync(nameof(ManagerTeamsPage));
        }
    }
}
