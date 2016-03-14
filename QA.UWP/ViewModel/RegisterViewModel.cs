using QA.UWP.Core;
using QA.UWP.Model;
using Account.Client;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Client;

namespace QA.UWP.ViewModel
{
    public class RegisterViewModel : MyViewModelBase
    {
        public RegisterViewModel()
        {
            IsRegistering = false;
            RegisterButtonIsEnable = true;
            RegisterUser = new RegisterUser();
        }

        private bool _isRegistering;
        public bool IsRegistering
        {
            get { return _isRegistering; }
            set
            {
                Set(ref _isRegistering, value);
                RegisterButtonIsEnable = !_isRegistering;
            }
        }



        private bool _registerButtonIsEnable;
        public bool RegisterButtonIsEnable
        {
            get { return _registerButtonIsEnable; }
            set { Set(ref _registerButtonIsEnable, value); }
        }

        public RegisterUser RegisterUser { get; set; }

        private string _registerErrorMessage;
        public string RegisterErrorMessage
        {
            get { return _registerErrorMessage; }
            set { Set(ref _registerErrorMessage, value); }
        }

        private RelayCommand _registerCommand;
        public RelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand(async () =>
                    {
                        IsRegistering = true;
                        RegisterErrorMessage = string.Empty;
                        HttpResult result;
                        using (var client = ClientFactory.CreateAccountClient())
                        {
                            result = await client.RegisterAsync(RegisterUser);
                            if (result != null && result.Succeeded)
                            {
                                HttpResult<AccessTokenResponse> loginResult = await client.LoginAsync(RegisterUser.UserName, RegisterUser.Password);
                                IsRegistering = false;
                                if(loginResult!=null&&loginResult.Succeeded)
                                {
                                    AppSettings settings = new AppSettings();
                                    settings.SavePasswordCredential(RegisterUser.UserName, RegisterUser.Password);
                                    settings.AccessToken = loginResult.Content.AccessToken;
                                    NavigationService.NavigateTo(typeof(QuestionsViewModel).FullName);
                                }else
                                {
                                    StringBuilder builder = new StringBuilder("Register success but login error:");
                                    foreach(var item in loginResult.Errors)
                                    {
                                        builder.Append(item).Append("\n");
                                    }
                                    RegisterErrorMessage = builder.ToString();
                                }
                            }
                            else
                            {
                                IsRegistering = false;
                                StringBuilder builder = new StringBuilder("Register error:");
                                foreach (var item in result.Errors)
                                {
                                    builder.Append(item).Append("\n");
                                }
                                RegisterErrorMessage = builder.ToString();
                            }
                        }
                    }, () => !IsRegistering));
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}
