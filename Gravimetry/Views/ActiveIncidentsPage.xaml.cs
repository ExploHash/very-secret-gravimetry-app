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
    public partial class ActiveIncidentsPage : ContentPage
    {
        ActiveIncidentsViewModel _viewModel;

        public ActiveIncidentsPage()
        {

            InitializeComponent();

            BindingContext = _viewModel = new ActiveIncidentsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.IsRefreshing = true;
            _viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.IsRefreshing = false;
        }
    }
}
