﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gravimetry.Models;
using Gravimetry.Views;
using Gravimetry.Services;

namespace Gravimetry.ViewModels
{
    public class TeamsViewModel : BaseViewModel //Needs to be based of BaseViewModel for stupid notify event handling
    {
        //Observable collection for items which are shown in view
        public ObservableCollection<Team> Items { get; }
        //Command called by refreshview when refreshing
        public Command LoadItemsCommand { get; }

        public Command<Team> LeaveTeam { get; }

        public Command GoToJoinTeamsView { get; }

        readonly UserService _userService = new UserService();


        public TeamsViewModel()
        {
            //Initialize observer
            Items = new ObservableCollection<Team>();
            //Initialize command and link to the method it actually executes when called
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LeaveTeam = new Command<Team>(async (team) => await OnLeaveTeam(team));
            GoToJoinTeamsView = new Command(async (obj) => await OnGoToJoinTeamsView());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true; //Set to busy so refreshing icon is shown (and blocks double reload)
            
            try
            {
                Items.Clear(); //Clear list first
                var items = await _userService.GetTeams(); //grab from api
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

        private async Task OnGoToJoinTeamsView()
        {
            await Shell.Current.GoToAsync(nameof(JoinTeamsPage)); //Open joins view
        }

        private async Task OnLeaveTeam(Team team)
        {
            await _userService.LeaveTeam(team.Id); //Call leave team
            IsBusy = true; //Trigger refresh
        }
    }
}
