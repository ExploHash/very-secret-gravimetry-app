using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravimetry.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Gravimetry.Services;
using Xamarin.Essentials;

namespace Gravimetry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        readonly AuthService _authService = new AuthService();

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            //Grab properties from binding
            string name = Username.Text;
            string password = Password.Text;

            //Try calling login
            var success = await _authService.SignIn(name, password);

            if (success)
            {
                //If success redirect
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                //If failed show message
                Error.Text = "Invalid Credentials";
            }


        }

        protected override void OnAppearing()
        {
            //Check for accesstoken
            var accessToken = Preferences.Get("accessToken", "");

            if (accessToken.Length > 0)
            {
                Application.Current.MainPage = new AppShell();
            }
        }
    }

}
