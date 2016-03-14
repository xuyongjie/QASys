using Account.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Storage;

namespace QA.UWP.Core
{
    public class AppSettings : IAccessTokenStore
    {
        const string AccessTokenKey = "AATravelAccessToken";
        const string TravelResource = "AATravel";

        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
        PasswordVault passwordVault = new PasswordVault();


        public string AccessToken
        {
            get
            {
                return roamingSettings.Values[AccessTokenKey] as string;
            }
            set
            {
                roamingSettings.Values[AccessTokenKey] = value;
            }
        }

        IEnumerable<PasswordCredential> GetPasswordCredentials()
        {
            return passwordVault.RetrieveAll().Where(cred => cred.Resource == TravelResource);
        }

        public PasswordCredential GetPasswordCredential()
        {
            return GetPasswordCredentials().FirstOrDefault();
        }

        public void ClearPasswordCredentials()
        {
            foreach (PasswordCredential passwordCredential in GetPasswordCredentials())
            {
                passwordVault.Remove(passwordCredential);
            }
        }

        public void ChangePasswordCredential(string username, string newPassword)
        {
            PasswordCredential passwordCredential = GetPasswordCredentials().Where(cred => cred.UserName == username).FirstOrDefault();
            if (passwordCredential != null)
            {
                passwordVault.Remove(passwordCredential);
                passwordCredential.Password = newPassword;
                passwordVault.Add(passwordCredential);
            }
        }

        public void SavePasswordCredential(string username, string password)
        {
            passwordVault.Add(new PasswordCredential(TravelResource, username, password));
        }

        public void LogOff()
        {
            AccessToken = null;
            ClearPasswordCredentials();
        }
    }
}
