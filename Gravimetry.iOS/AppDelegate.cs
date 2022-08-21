using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Gravimetry.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());


            if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
            {
                var appearance = new UINavigationBarAppearance()
                {
                    BackgroundColor = UIColor.FromRGB(150, 209, 255),
                    ShadowColor = UIColor.FromRGB(150, 209, 255),
                };

                UINavigationBar.Appearance.StandardAppearance = appearance;
                UINavigationBar.Appearance.ScrollEdgeAppearance = appearance;

                var appearance2 = new UITabBarAppearance()
                {
                    BackgroundColor = UIColor.FromRGB(150, 209, 255),
                    ShadowColor = UIColor.FromRGB(150, 209, 255),
                };

                UITabBar.Appearance.StandardAppearance = appearance2;
                UITabBar.Appearance.ScrollEdgeAppearance = appearance2;


                UITabBar.Appearance.TintColor = UIColor.White;
                UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White });
                UITabBar.Appearance.SelectedImageTintColor = UIColor.White;
                UITabBar.Appearance.UnselectedItemTintColor = UIColor.White;

                ///END OF WORKAROUND
            }

            return base.FinishedLaunching(app, options);
        }
    }
}
