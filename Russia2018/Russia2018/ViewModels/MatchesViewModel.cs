


namespace Russia2018.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Russia2018.Helpers;
    using Russia2018.Models;
    using Russia2018.Services;
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
        private string _filter;

        #endregion

        #region Properties

        public string Filter
        {
            get { return _filter; }
            set
            {
                this.SetValue(ref _filter, value);
                this.Search();
            }
        }

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

        #region Commands

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(LoadMatches); }
        }

        public ICommand SearchCommand
        {
            get { return new RelayCommand(Search); }
        }

        private void Search()
        {
            //When the Filter is null we have to load the whole list
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Matches = new ObservableCollection<Match>(this.myMatches);
            }
            else
            {
                this.Matches = new ObservableCollection<Match>(this.myMatches.Where(
                    m => m.Home.Name.ToLower().Contains(this.Filter.ToLower()) ||
                         m.Visitor.Name.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}
