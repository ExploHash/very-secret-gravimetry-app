using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gravimetry.Models;
using Gravimetry.Services;
using Xamarin.Essentials;

namespace Gravimetry.ViewModels
{
    public class JoinTeamsViewModel : BaseViewModel //Needs to be based of BaseViewModel for stupid notify event handling
    {
        //Observable collection for items which are shown in view
        public ObservableCollection<Team> Items { get; }
        //Command called by refreshview when refreshing
        public Command LoadItemsCommand { get; }

        public Command<Team> JoinTeam { get; }

        public Command ScanQr { get; }

        readonly TeamsService _teamsService;
        readonly UserService _userService;


        public JoinTeamsViewModel(
            TeamsService teamService,
            UserService userService
        )
        {
            _teamsService = teamService;
            _userService = userService;
            //Initialize observer
            Items = new ObservableCollection<Team>();
            //Initialize commands
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            JoinTeam = new Command<Team>(OnJoinTeam);
            ScanQr = new Command(async () => await OnScanQr());
        }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true; //Set to busy so refreshing icon is shown (and blocks double reload)
            
            try
            {
                Items.Clear(); //Clear list first
                var items = await _teamsService.GetPublicTeams(); //grab from api
                var userItems = await _userService.GetTeams();

                foreach (var item in items) //Loop through and set them in the observablecollection
                {
                    //Check if user already has team in list
                    if (userItems.Find(userItem => userItem.Id == item.Id) != null)
                    {
                        item.UserJoined = true; //if so set userjoined to true
                    }
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

        private async void OnJoinTeam(Team team)
        {
            if (team.UserJoined) return; //Early exit if clicked team is already joined
            await _userService.JoinTeam(team.Id); //Call join team
            IsBusy = true; //Trigger refresh
        }

        private async Task OnScanQr()
        {
            //Open photo dialog
            await MediaPicker.CapturePhotoAsync();
        }
    }
}
