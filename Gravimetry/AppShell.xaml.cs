﻿using System;
using System.Collections.Generic;
using Gravimetry.ViewModels;
using Gravimetry.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gravimetry
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}
