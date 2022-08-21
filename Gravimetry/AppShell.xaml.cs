using System;
using System.Collections.Generic;
using Gravimetry.ViewModels;
using Gravimetry.Views;
using Gravimetry.Views.Manager;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gravimetry
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(JoinTeamsPage), typeof(JoinTeamsPage));
            Routing.RegisterRoute(nameof(MonitorDetailPage), typeof(MonitorDetailPage));
            Routing.RegisterRoute(nameof(IncidentDetailPage), typeof(IncidentDetailPage));
            Routing.RegisterRoute(nameof(ManagerTeamsPage), typeof(ManagerTeamsPage));
            Routing.RegisterRoute(nameof(ManagerCreateTeamPage), typeof(ManagerCreateTeamPage));
            Routing.RegisterRoute(nameof(ManagerUpdateTeamPage), typeof(ManagerUpdateTeamPage));
            Routing.RegisterRoute(nameof(ManagerAddUsersPage), typeof(ManagerAddUsersPage));
            Routing.RegisterRoute(nameof(ManagerAddMonitorsPage), typeof(ManagerAddMonitorsPage));
        }
    }
}
