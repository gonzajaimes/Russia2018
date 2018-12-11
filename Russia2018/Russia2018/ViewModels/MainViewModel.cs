﻿

namespace Russia2018.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;
    using Helpers;
    using Models;

    public class MainViewModel : BaseViewModel
    {
        #region Attributes

        private UserLocal _user;

        #endregion

        #region Properties
        public List<Land> LandsList
        {
            get;
            set;
        }

        public TokenResponse Token
        {
            get;
            set;
        }

        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get;
            set;
        }

        public UserLocal User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }

        #endregion

        #region ViewModels

        public ChangePasswordViewModel ChangePassword
        {
            get;
            set;
        }

        public LoginViewModel Login
        {
            get;
            set;
        }

        public LandsViewModel Lands
        {
            get;
            set;
        }

        public LandViewModel Land
        {
            get;
            set;
        }
        public RegisterViewModel Register
        {
            get;
            set;
        }

        public MyProfileViewModel MyProfile
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }


        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.MenuItems = new ObservableCollection<MenuItemViewModel>();
            this.MenuItems.Add(new MenuItemViewModel
            {
                Icon = "ic_settings",
                PageName = "MyProfilePage",
                Title = Languages.MyProfile,
            });
            this.MenuItems.Add(new MenuItemViewModel
            {
                Icon = "ic_insert_chart",
                PageName = "StatsPage",
                Title = Languages.Stats,
            });
            this.MenuItems.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = Languages.LogOut,
            });
        }
        #endregion
    }
}
