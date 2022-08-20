using System;
using Gravimetry.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Gravimetry.Services;
using Xamarin.Essentials;

namespace Gravimetry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncidentDetailPage : ContentPage
    {
        private IncidentDetailViewModel _viewModel;

        public IncidentDetailPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new IncidentDetailViewModel();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.IsRefreshing = false;
        }
    }

}
