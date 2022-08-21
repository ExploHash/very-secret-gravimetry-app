using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Gravimetry.Models;
using Gravimetry.Views;
using Gravimetry.Services;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web;

namespace Gravimetry.ViewModels.Manager
{
    public class ManagerAddUsersViewModel : BaseViewModel, IQueryAttributable //Needs to be based of BaseViewModel for stupid notify event handling
    {
        //Observable collection for items which are shown in view
        public ObservableCollection<ApplicationUser> Items { get; }
        //Command called by refreshview when refreshing
        public Command LoadItemsCommand { get; }

        public Command<ApplicationUser> AddUser { get; }

        readonly ManagerService _managerService = new ManagerService();

        private Team _team;

        public Team team
        {
            get { return _team; }
            set
            {
                _team = value;
                OnPropertyChanged();
            }
        }

        int _teamId;

        public int TeamId
        {
            get
            {
                return _teamId;
            }
            set
            {
                _teamId = value;
                LoadTeam(value);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            TeamId = int.Parse(HttpUtility.UrlDecode(query["TeamId"]));
        }

        public async Task LoadTeam(int teamId)
        {
            try
            {
                team = await _managerService.GetTeam(teamId); //grab from api
                IsBusy = true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public ManagerAddUsersViewModel()
        {
            //Initialize observer
            Items = new ObservableCollection<ApplicationUser>();
            //Initialize command and link to the method it actually executes when called
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddUser = new Command<ApplicationUser>(async (user) => await OnAddUser(user));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true; //Set to busy so refreshing icon is shown (and blocks double reload)
            
            try
            {
                Items.Clear(); //Clear list first
                var items = await _managerService.GetAllUsers();
                var userItems = team.ApplicationUsers;

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

        private async Task OnAddUser(ApplicationUser user)
        {
            if (user.UserJoined) return; //Early exit if clicked team is already joined
            await _managerService.AddUser(TeamId, user.Id);
            await LoadTeam(TeamId);
            IsBusy = true; //Trigger refresh
        }
    }
}
