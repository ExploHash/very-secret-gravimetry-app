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

namespace Gravimetry.ViewModels.Manager
{
    public class ManagerTeamsViewModel : BaseViewModel //Needs to be based of BaseViewModel for stupid notify event handling
    {
        //Observable collection for items which are shown in view
        public ObservableCollection<Team> Items { get; }
        //Command called by refreshview when refreshing
        public Command LoadItemsCommand { get; }

        public Command<Team> ItemTapped { get; }

        public Command GoToCreatePage { get; }

        readonly ManagerService _managerService = new ManagerService();


        public ManagerTeamsViewModel()
        {
            //Initialize observer
            Items = new ObservableCollection<Team>();
            //Initialize command and link to the method it actually executes when called
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Team>(async (team) => await OnItemTapped(team));
            GoToCreatePage = new Command(async () => await OnGoToCreatePage());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true; //Set to busy so refreshing icon is shown (and blocks double reload)
            
            try
            {
                Items.Clear(); //Clear list first
                var items = await _managerService.GetAllTeams(); //grab from api
                foreach (var item in items) //Loop through and set them in the observablecollection
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false; //Always (error or not) set to IsBusy false
            }
        }

        public void OnAppearing() //Intialize function called from view
        {
            IsBusy = true; //Trigger a ExecuteLoadItemsCommand command by setting to true
        }

        private async Task OnItemTapped(Team team)
        {
            await Shell.Current.GoToAsync($"{nameof(ManagerUpdateTeamPage)}?TeamId={team.Id}");
        }

        private async Task OnGoToCreatePage()
        {
            await Shell.Current.GoToAsync(nameof(ManagerCreateTeamPage));
        }
    }
}
