using Account.Client;
using GalaSoft.MvvmLight.Command;
using QA.UWP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.UWP.ViewModel
{
    public class UserInfoViewModel:MyViewModelBase,INavigatable
    {
        public UserInfoViewModel()
        {

        }

        private UserInfo _myInfo;
        public UserInfo MyInfo
        {
            get { return _myInfo; }
            set { Set(ref _myInfo, value); }
        }

        public void NavigateTo(object parameter)
        {
            MyInfo = parameter as UserInfo;
        }

        public void NavigateFrom(object parameter)
        {
        }

        private RelayCommand _exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new RelayCommand(() =>
                {
                    AppSettings settings = new AppSettings();
                    settings.LogOff();
                    NavigationService.NavigateTo(typeof(LoginViewModel).FullName);

                }));
            }
        }
    }
}
