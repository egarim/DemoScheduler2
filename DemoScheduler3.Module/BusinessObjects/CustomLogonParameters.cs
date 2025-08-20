using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System;

namespace DemoScheduler3.Module.BusinessObjects
{
    [DomainComponent]
    [Serializable]
    [System.ComponentModel.DisplayName("Log In")]
    public class CustomLogonParameters : INotifyPropertyChanged {
        private string userName;
        private string password;
        private bool showPassword;

        public CustomLogonParameters()
        {
            this.showPassword = true;
        }
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
        [ImmediatePostData()]
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
        
        // Method to refresh persistent objects when needed
        public void RefreshPersistentObjects(DevExpress.ExpressApp.IObjectSpace objectSpace) {
            // Nothing to refresh in this simple implementation
        }
    }
}