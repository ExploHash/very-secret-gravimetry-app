using System;
using Gravimetry.ViewModels;
using Gravimetry.ViewModels.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Gravimetry.Services;
using Xamarin.Essentials;

namespace Gravimetry.Views.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerUpdateTeamPage : ContentPage
    {

        public ManagerUpdateTeamPage()
        {
            InitializeComponent();
            this.BindingContext = new ManagerUpdateTeamViewModel();
        }
    }

}
