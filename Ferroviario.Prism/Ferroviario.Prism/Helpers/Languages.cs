using Ferroviario.Common.Interfaces;
using Ferroviario.Prism.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Ferroviario.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string Name => Resource.Name;

        public static string Loading => Resource.Loading;

        public static string Changes => Resource.Changes;

        public static string MyRequests => Resource.MyRequests;

        public static string Service => Resource.Service;

        public static string MyShifts => Resource.MyShifts;

        public static string User => Resource.User;

        public static string ShiftDetail => Resource.ShiftDetail;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string EmailError => Resource.EmailError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

        public static string Login => Resource.Login;

        public static string LoginError => Resource.LoginError;

        public static string Logout => Resource.Logout;

    }

}
