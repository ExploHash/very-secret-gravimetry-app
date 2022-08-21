using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Gravimetry.Models;
using Gravimetry.Views;
using Gravimetry.Services;
using System.ComponentModel;

namespace Gravimetry.ViewModels.Manager
{
    public class ManagerCreateTeamViewModel : BaseViewModel
    {
        public Command CreateCommand { get; }

        readonly ManagerService _managerService = new ManagerService();

        private TeamInput _teamInput;

        public TeamInput teamInput
        {
            get { return _teamInput; }
            set
            {
                _teamInput = value;
                OnPropertyChanged();
            }
        }

        public ManagerCreateTeamViewModel()
        {
            CreateCommand = new Command(async () => await OnCreate());
            _teamInput = new TeamInput();
        }

        public async Task OnCreate()
        {
            var team = await _managerService.Create(teamInput);
            await Shell.Current.Navigation.PopAsync();
        }

    }
}
