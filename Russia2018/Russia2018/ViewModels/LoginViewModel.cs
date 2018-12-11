﻿
namespace Russia2018.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Helpers;
    using System;

    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiservice;
        private DataService dataservice;

        #endregion

        #region Attributes
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private string _email;
        #endregion

        #region Properties
        public string Email
        {
            get { return _email; }
            set { SetValue(ref _email, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetValue(ref _isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetValue(ref _isEnabled, value); }
        }


        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiservice = new ApiService();
            this.dataservice = new DataService();
            this.IsRemembered = true;
            this.IsEnabled = true;

        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }

        }


        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;

            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept);
                return;

            }
            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiservice.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;

            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var token = await this.apiservice.GetToken(
                               apiSecurity,
                               this.Email,
                               this.Password);
            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SomethingWrong,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LoginError,
                    Languages.Accept);
                this.Password = string.Empty;
                return;
            }

            var user = await this.apiservice.GetUserByEmail(
                               apiSecurity,
                               "/api",
                               "/Users/GetUserByEmail",
                               token.TokenType,
                               token.AccessToken,
                               this.Email);

            var userLocal = Converter.ToUserLocal(user);
            userLocal.Password = this.Password;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.User = userLocal;

            if (this.IsRemembered)
            {
                Settings.IsRemembered = "true";
            }
            else
            {
                Settings.IsRemembered = "false";
            }

            //guardamos el usuario y token en persistencia al hacer Login.
            this.dataservice.DeleteAllAndInsert(userLocal);
            this.dataservice.DeleteAllAndInsert(token);

            mainViewModel.Lands = new LandsViewModel();

            Application.Current.MainPage = new MasterPage();
            //await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;


        }

        public ICommand LoginFacebookComand
        {
            get
            {
                return new RelayCommand(LoginFacebook);
            }
        }

        private async void LoginFacebook()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginFacebookPage());
        }


        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }

        }

        private async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
        #endregion


    }
}
