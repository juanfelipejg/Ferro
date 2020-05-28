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

        public static string DocumentError => Resource.DocumentError;

        public static string FirstNameError => Resource.FirstNameError;

        public static string LastNameError => Resource.LastNameError;

        public static string Address => Resource.Address;

        public static string AddressError => Resource.AddressError;

        public static string AddressPlaceHolder => Resource.AddressPlaceHolder;

        public static string Phone => Resource.Phone;

        public static string PhoneError => Resource.PhoneError;

        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordConfirmError1 => Resource.PasswordConfirmError1;

        public static string PasswordConfirmError2 => Resource.PasswordConfirmError2;

        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;

        public static string Ok => Resource.ok;

        public static string PictureSource => Resource.PictureSource;

        public static string Cancel => Resource.Cancel;

        public static string FromCamera => Resource.FromCamera;

        public static string FromGallery => Resource.FromGallery;

        public static string PasswordRecover => Resource.PasswordRecover;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string Save => Resource.Save;

        public static string ChangePassword => Resource.ChangePassword;

        public static string ModifyUser => Resource.ModifyUser;

        public static string UserUpdated => Resource.UserUpdated;

        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;

        public static string ConfirmNewPasswordError => Resource.ConfirmNewPasswordError;

        public static string ConfirmNewPasswordError2 => Resource.ConfirmNewPasswordError2;

        public static string ConfirmNewPasswordPlaceHolder => Resource.ConfirmNewPasswordPlaceHolder;

        public static string CurrentPassword => Resource.CurrentPassword;

        public static string CurrentPasswordError => Resource.CurrentPasswordError;

        public static string CurrentPasswordPlaceHolder => Resource.CurrentPasswordPlaceHolder;

        public static string NewPassword => Resource.NewPassword;

        public static string NewPasswordError => Resource.NewPasswordError;

        public static string NewPasswordPlaceHolder => Resource.NewPasswordPlaceHolder;

        public static string Type => Resource.Type;

        public static string Dates => Resource.Dates;

        public static string State => Resource.State;

        public static string Details => Resource.Details;

        public static string Holidays => Resource.Holidays;

        public static string License => Resource.License;

        public static string FreeDay => Resource.FreeDay;

        public static string AcademicPermission => Resource.AcademicPermission;

        public static string Others => Resource.Others;

        public static string YourDescription => Resource.YourDescription;

        public static string AdminComment => Resource.AdminComment;

        public static string RequestDetail => Resource.RequestDetail;

        public static string SearchChange => Resource.SearchChange;

        public static string Driver => Resource.Driver;

        public static string Start => Resource.Start;

        public static string End => Resource.End;

        public static string Date => Resource.Date;

        public static string CreateChange => Resource.CreateChange;

        public static string NewRequest => Resource.NewRequest;

    }

}
