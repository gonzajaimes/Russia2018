﻿
namespace Russia2018.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    [Activity(
        Label = "Russia2018", 
        Icon = "@drawable/ic_launcher", 
        Theme = "@style/MainTheme", 
        MainLauncher = false, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTE4MzdAMzEzNjJlMzQyZTMwVXZhQ1BFQWVmWVl6N0pyUC9RaVB0WmVmSEdXN2NENStCM0hiUGl2YWdTST0=");

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}