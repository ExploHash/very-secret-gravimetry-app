using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Gravimetry.Models;
using Gravimetry.Views;
using Gravimetry.ViewModels;
using Gravimetry.Services;
using Xamarin.Essentials;


namespace Gravimetry.Views
{
    public partial class MonitorOverviewPage : ContentPage
    {
        MonitorsViewModel _viewModel;

        public MonitorOverviewPage()
        {

            InitializeComponent();

            BindingContext = _viewModel = new MonitorsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(async () =>
            {
                await _viewModel.ExecuteLoadItemsCommand();
            });
        }

        private void OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            // Has Backspace or Cancel has been pressed?
            if (textChangedEventArgs.NewTextValue == string.Empty)
            {
                Task.Run(async () =>
                {
                    await _viewModel.ExecuteLoadItemsCommand();
                });
            }
        }

    }
}
