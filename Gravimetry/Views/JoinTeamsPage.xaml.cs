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
    public partial class JoinTeamsPage : ContentPage
    {
        JoinTeamsViewModel _viewModel;

        public JoinTeamsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new JoinTeamsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
