using QA.UWP.ViewModel;
using System.Runtime.Serialization;

namespace QA.UWP.Model
{
    [DataContract]
    public class RegisterEntity:MyViewModelBase
    {
        private string _email;
        [DataMember(Name ="Email")]
        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }
        private string _password;
        [DataMember(Name ="Password")]
        public string Password
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }
        private string _confirmPassword;
        [DataMember(Name ="ConfirmPassword")]
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { Set(ref _confirmPassword, value); }
        }
    }
}
