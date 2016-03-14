using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.UWP.Model
{
    public class User:ViewModelBase
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { Set(ref _userName, value); }
        }

        private string _avatar;
        public string Avatar
        {
            get { return _avatar; }
            set { Set(ref _avatar, value); }
        }
    }
}
