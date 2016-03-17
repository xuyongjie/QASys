using QA.UWP.Core;
using QA.UWP.Model;
using QA.UWP.View;
using Account.Client;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Client;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Microsoft.Practices.ServiceLocation;

namespace QA.UWP.ViewModel
{
    public class LoginViewModel : MyViewModelBase,INavigatable
    {
        public LoginViewModel()
        {
            IsLogining = false;
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { Set(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }

        private bool _isLogining;
        public bool IsLogining
        {
            get { return _isLogining; }
            set
            {
                Set(ref _isLogining, value);
                LoginButtonIsEnable = !_isLogining;
            }
        }


        private bool _loginButtonIsEnable;
        public bool LoginButtonIsEnable
        {
            get { return _loginButtonIsEnable; }
            set { Set(ref _loginButtonIsEnable, value); }
        }


        private string _loginErrorMessage;
        public string LoginErrorMessage
        {
            get { return _loginErrorMessage; }
            set { Set(ref _loginErrorMessage, value); }
        }

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(async () =>
              {
                  IsLogining = true;
                  LoginErrorMessage = string.Empty;
                  HttpResult<AccessTokenResponse> result;
                  using (var client = ClientFactory.CreateAccountClient())
                  {
                      result = await client.LoginAsync(UserName, Password);
                      IsLogining = false;
                  }
                  if (result != null && result.Succeeded)
                  {
                      //Login success
                      AppSettings settings = new AppSettings();
                      settings.SavePasswordCredential(UserName, Password);
                      LoginErrorMessage = result.Content.AccessToken;
                      settings.AccessToken = result.Content.AccessToken;
                      NavigationService.NavigateTo(typeof(QuestionsViewModel).FullName);
                  }
                  else
                  {
                      StringBuilder builder = new StringBuilder("ErrorMessage:");
                      foreach(var item in result.Errors)
                      {
                          builder.Append(item).Append("\t");
                      }
                      LoginErrorMessage = builder.ToString();
                  }
              }, () => !IsLogining));
            }
        }

        private RelayCommand _registerCommand;
        public RelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand(() =>
                    {
                        NavigationService.NavigateTo(typeof(RegisterViewModel).FullName);
                    }));
            }
        }

        public void NavigateTo(object parameter)
        {
            (Window.Current.Content as Frame).BackStack.Clear();
        }

        public void NavigateFrom(object parameter)
        {

        }
    }
}
