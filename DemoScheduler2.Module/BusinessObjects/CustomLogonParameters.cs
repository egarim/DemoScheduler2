using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace DemoScheduler2.Module.BusinessObjects {
    [DomainComponent]
    public class CustomLogonParameters : INotifyPropertyChanged {
        private string userName;
        private string password;
        private bool showPassword;

        [ModelDefault("Caption", "User Name")]
        public string UserName {
            get { return userName; }
            set {
                if (userName != value) {
                    userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        [ModelDefault("Caption", "Password")]
        [PasswordPropertyText(true)]
        public string Password {
            get { return password; }
            set {
                if (password != value) {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        [ModelDefault("Caption", "Show Password")]
        public bool ShowPassword {
            get { return showPassword; }
            set { 
                if (showPassword != value) {
                    showPassword = value;
                    OnPropertyChanged(nameof(ShowPassword));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}