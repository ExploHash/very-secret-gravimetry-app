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
    public class ManagerAddMonitorViewModel : BaseViewModel, IQueryAttributable //Needs to be based of BaseViewModel for stupid notify event handling
    {
        //Observable collection for items which are shown in view
        public ObservableCollection<SiteMonitor> Items { get; }
        //Command called by refreshview when refreshing
        public Command LoadItemsCommand { get; }

        public Command<SiteMonitor> AddMonitor { get; }

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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public ManagerAddMonitorViewModel()
        {
            //Initialize observer
            Items = new ObservableCollection<SiteMonitor>();
            //Initialize command and link to the method it actually executes when called
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddMonitor = new Command<SiteMonitor>(async (monitor) => await OnAddMonitor(monitor));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true; //Set to busy so refreshing icon is shown (and blocks double reload)
            
            try
            {
                Items.Clear(); //Clear list first
                var items = await _managerService.GetAllMonitors();
                var userItems = team.SiteMonitors;

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
            //ExecuteLoadItemsCommand();
        }

        private async Task OnAddMonitor(SiteMonitor monitor)
        {
            if (monitor.UserJoined) return; //Early exit if clicked team is already joined
            await _managerService.AddMonitor(TeamId, monitor.Id);
            await LoadTeam(TeamId);
            IsBusy = true; //Trigger refresh
        }
    }
}
