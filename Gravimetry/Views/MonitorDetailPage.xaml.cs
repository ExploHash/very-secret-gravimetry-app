using System;
using Gravimetry.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Gravimetry.Services;
using Xamarin.Essentials;

namespace Gravimetry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonitorDetailPage : ContentPage
    {
        public MonitorDetailPage()
        {
            InitializeComponent();
            this.BindingContext = new MonitorDetailViewModel();
        }
    }

}
