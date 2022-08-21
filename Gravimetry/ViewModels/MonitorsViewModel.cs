using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gravimetry.Models;
using Gravimetry.Views;
using Gravimetry.Services;

namespace Gravimetry.ViewModels
{
    public class MonitorsViewModel //Needs to be based of BaseViewModel for stupid notify event handling
    {
        //Observable collection for items which are shown in view
        public ObservableCollection<SiteMonitor> Items { get; }
        //Command called by refreshview when refreshing
        public Command<string> LoadItemsCommand { get; }

        public Command<SiteMonitor> ItemTapped { get; }

        readonly UserService _userService = new UserService();


        public MonitorsViewModel()
        {
            //Initialize observer
            Items = new ObservableCollection<SiteMonitor>();
            //Initialize command and link to the method it actually executes when called
            LoadItemsCommand = new Command<string>(async (text) => await ExecuteLoadItemsCommand(text));

            ItemTapped = new Command<SiteMonitor>(async(monitor) => await OnItemTapped(monitor));
        }

        public async Task ExecuteLoadItemsCommand(string search = "")
        {
            Items.Clear(); //Clear list first
            var items = await _userService.GetMonitors(search); //grab from api
            
            foreach (var item in items) //Loop through and set them in the observablecollection
            {
                Items.Add(item);
            }
        }

        private async Task OnItemTapped(SiteMonitor siteMonitor)
        {
            await Shell.Current.GoToAsync($"{nameof(MonitorDetailPage)}?MonitorId={siteMonitor.Id}");
        }
    }
}
