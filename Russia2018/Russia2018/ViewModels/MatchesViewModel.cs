


namespace Russia2018.ViewModels
{
    using Russia2018.Helpers;
    using Russia2018.Models;
    using Russia2018.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class MatchesViewModel : BaseViewModel
    {
        #region Services

        private ApiService apiService;

        #endregion

        #region Attributes

        private ObservableCollection<Match> _matches;
        private List<Match> myMatches;
        private bool _isRefreshing;

        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { this.SetValue(ref _isRefreshing, value); }
        }

        public ObservableCollection<Match> Matches
        {
            get { return _matches; }
            set { this.SetValue(ref _matches, value); }
        }
        #endregion

        #region Constructors

        public MatchesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadMatches();
        }

        #endregion

        #region Methods

        private async void LoadMatches()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
               this.IsRefreshing = false;
               await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.GetList<Match>(
                apiSecurity,
                "/api", "/Matches",
                MainViewModel.GetInstance().Token.TokenType,
                MainViewModel.GetInstance().Token.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                     connection.Message,
                     Languages.Accept);
                return;             
            }

            this.myMatches = (List<Match>)response.Result;
            this.myMatches.ForEach(m => m.DateTime = m.DateTime.ToLocalTime());
            this.Matches = new ObservableCollection<Match>(this.myMatches);
            this.IsRefreshing = false;
        }

        #endregion
    }
}
