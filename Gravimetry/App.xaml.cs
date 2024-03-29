﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Gravimetry.Services;
using Gravimetry.Views;

[assembly: ExportFont("faregular.ttf", Alias = "FontAwesome")]
[assembly: ExportFont("fasolid.ttf", Alias = "FontAwesomeSolid")]

namespace Gravimetry
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
