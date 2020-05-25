using Ferroviario.Common.Helpers;
using Ferroviario.Common.Models;
using Ferroviario.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ferroviario.Prism.ViewModels
{
        public class FerroviarioMasterDetailPageViewModel : ViewModelBase
        {
            private readonly INavigationService _navigationService;
            private UserResponse _user;

            public FerroviarioMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
            {
                _navigationService = navigationService;
                 LoadUser();
                 LoadMenus();
            }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }


        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

            private void LoadMenus()
            {
                List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "request",
                    PageName = "RequestsPage",
                    Title = Languages.MyRequests
                },
                new Menu
                {
                    Icon = "shift",
                    PageName = "ShiftsPage",
                    Title = Languages.MyShifts
                },
                new Menu
                {
                    Icon = "user",
                    PageName = "UsersPage",
                    Title = Languages.User
                },
                new Menu
                {
                 Icon = "login",
                 PageName = "LoginPage",
                 Title = Settings.IsLogin ? Languages.Logout : Languages.Login
                }

            };

                Menus = new ObservableCollection<MenuItemViewModel>(
                    menus.Select(m => new MenuItemViewModel(_navigationService)
                    {
                        Icon = m.Icon,
                        PageName = m.PageName,
                        Title = m.Title
                    }).ToList());
            }
        }

    }

