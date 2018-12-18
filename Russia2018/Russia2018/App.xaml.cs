
namespace Russia2018
{
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Views;
    using Helpers;
    using ViewModels;
    using Services;
    using Models;

    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTE4MzdAMzEzNjJlMzQyZTMwVXZhQ1BFQWVmWVl6N0pyUC9RaVB0WmVmSEdXN2NENStCM0hiUGl2YWdTST0=");

            InitializeComponent();

           this.MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
