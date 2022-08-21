using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gravimetry.Models;
using Gravimetry.Services;
using Gravimetry.Views.Manager;
using System.Collections.Generic;
using System.Web;

namespace Gravimetry.ViewModels.Manager
{
    public class ManagerUpdateTeamViewModel : BaseViewModel, IQueryAttributable
    {
        readonly ManagerService _managerService = new ManagerService();

        public Command UpdateCommand { get; }

        public Command<SiteMonitor> DeleteMonitor { get; }

        public Command<ApplicationUser> DeleteUser { get; }

        public Command AddUser { get; }

        public Command AddMonitor { get; }

        public Command DeleteTeam { get; }

        public Command GoToTeamQRPage { get; }


        public ManagerUpdateTeamViewModel()
        {
            //Initialize commands
            UpdateCommand = new Command(async () => await UpdateTeam());
            AddUser = new Command(async () => await OnAddUser());
            AddMonitor = new Command(async () => await OnAddMonitor());
            DeleteTeam = new Command(async () => await OnDeleteTeam());
            GoToTeamQRPage = new Command(async () => await OnGoToTeamQRPage());

            DeleteMonitor = new Command<SiteMonitor>(async (monitor) => await OnDeleteMonitor(monitor));
            DeleteUser = new Command<ApplicationUser>(async (user) => await OnDeleteUser(user));
        }

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

        bool initialized = false;

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
                initialized = true;
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
                team = await _managerService.GetTeam(teamId); //grab team from api
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void OnAppearing()
        {
            if(initialized)
            {
                LoadTeam(TeamId);
            }
        }

        public async Task UpdateTeam()
        {
            await _managerService.Update(team.Id, team.Name, team.IsPublic);
        }

        public async Task OnDeleteUser(ApplicationUser applicationUser)
        {
            await _managerService.DeleteUser(TeamId, applicationUser.Id);
            await LoadTeam(TeamId);
        }

        public async Task OnDeleteMonitor(SiteMonitor monitor)
        {
            await _managerService.DeleteMonitor(TeamId, monitor.Id);
            await LoadTeam(TeamId);
        }

        private async Task OnAddMonitor()
        {
            await Shell.Current.GoToAsync($"{nameof(ManagerAddMonitorsPage)}?TeamId={TeamId}");
        }

        private async Task OnAddUser()
        {
            await Shell.Current.GoToAsync($"{nameof(ManagerAddUsersPage)}?TeamId={TeamId}");
        }

        private async Task OnGoToTeamQRPage()
        {
            await Shell.Current.GoToAsync($"{nameof(ManagerTeamQRPage)}?TeamId={TeamId}");

        }

        private async Task OnDeleteTeam()
        {
            await _managerService.Delete(TeamId);
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
