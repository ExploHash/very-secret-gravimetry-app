using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravimetry.ViewModels.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Gravimetry.Services;
using Xamarin.Essentials;

namespace Gravimetry.Views.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerCreateTeamPage : ContentPage
    {
        ManagerCreateTeamViewModel _viewModel;

        public ManagerCreateTeamPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new ManagerCreateTeamViewModel();
        }
    }

}
