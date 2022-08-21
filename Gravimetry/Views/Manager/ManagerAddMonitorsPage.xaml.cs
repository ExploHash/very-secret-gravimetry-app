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
using Gravimetry.ViewModels.Manager;
using Gravimetry.Services;
using Xamarin.Essentials;


namespace Gravimetry.Views.Manager
{
    public partial class ManagerAddMonitorsPage : ContentPage
    {
        ManagerAddMonitorViewModel _viewModel;

        public ManagerAddMonitorsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ManagerAddMonitorViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
