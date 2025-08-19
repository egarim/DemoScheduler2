using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DemoScheduler2.Module.BusinessObjects;

namespace DemoScheduler2.Module.Controllers {
    // This controller will be active on the logon view
    public class CustomLogonViewController : ViewController<DetailView> {
        private const string LogonActionId = "Logon";
        
        protected override void OnActivated() {
            base.OnActivated();
            
            // Subscribe to the Current Object's PropertyChanged event
            var logonParameters = View.CurrentObject as CustomLogonParameters;
            if (logonParameters != null) {
                logonParameters.PropertyChanged += LogonParameters_PropertyChanged;
                
                // Apply initial ShowPassword setting
                UpdatePasswordVisibility();
            }
            
            // Find the Logon action and modify its behavior if needed
            var dialogController = Frame.GetController<DialogController>();
            if (dialogController != null) {
                var logonAction = dialogController.AcceptAction;
                if (logonAction != null && logonAction.Id == LogonActionId) {
                    // Customize logon action if needed
                }
            }
        }
        
        protected override void OnDeactivated() {
            // Unsubscribe from events when the controller is deactivated
            var logonParameters = View.CurrentObject as CustomLogonParameters;
            if (logonParameters != null) {
                logonParameters.PropertyChanged -= LogonParameters_PropertyChanged;
            }
            
            base.OnDeactivated();
        }
        
        private void LogonParameters_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(CustomLogonParameters.ShowPassword)) {
                // When ShowPassword changes, update the password editor
                UpdatePasswordVisibility();
            }
        }
        
        private void UpdatePasswordVisibility() {
            // Apply the ShowPassword value to the password editor
            LogonParametersHelper.ApplyShowPasswordToEditor(View);
        }
    }
}