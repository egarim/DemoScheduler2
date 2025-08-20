using DemoScheduler3.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;

namespace DemoScheduler3.Module.Controllers
{
    // This controller will be active on the logon view
    public class CustomLogonViewController : ViewController<DetailView> {
        public CustomLogonViewController() {
           
            TargetObjectType = typeof(CustomLogonParameters);
        }
        
        protected override void OnActivated() {
            base.OnActivated();
            
            // Subscribe to the Current Object's PropertyChanged event
            if (View.CurrentObject is CustomLogonParameters logonParameters) {
                logonParameters.PropertyChanged += LogonParameters_PropertyChanged;
                
                // Apply initial ShowPassword setting
                UpdatePasswordVisibility();
            }
            
            // Optional: Subscribe to view events
            View.CurrentObjectChanged += View_CurrentObjectChanged;
            
            // Find the Logon action and modify its behavior if needed
            var dialogController = Frame.GetController<DialogController>();
            if (dialogController != null) {
                SimpleAction acceptAction = dialogController.AcceptAction;
                if (acceptAction != null) {
                    // You can customize the logon action here if needed
                    // For example, add validation or modify the caption
                    acceptAction.Caption = "Log In";
                }
            }
        }
        
        private void View_CurrentObjectChanged(object sender, EventArgs e) {
            // When the current object changes, update subscriptions
            if (View.CurrentObject is CustomLogonParameters logonParameters) {
                logonParameters.PropertyChanged += LogonParameters_PropertyChanged;
                UpdatePasswordVisibility();
            }
        }
        
        protected override void OnDeactivated() {
            // Unsubscribe from events when the controller is deactivated
            if (View.CurrentObject is CustomLogonParameters logonParameters) {
                logonParameters.PropertyChanged -= LogonParameters_PropertyChanged;
            }
            
            View.CurrentObjectChanged -= View_CurrentObjectChanged;
            
            base.OnDeactivated();
        }
        
        private void LogonParameters_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(CustomLogonParameters.ShowPassword)) {
                // When ShowPassword changes, update the password editor
                UpdatePasswordVisibility();
            }
        }
        
        private void UpdatePasswordVisibility() {
            //this.View.CurrentObject as CustomLogonParameters;
            // Apply the ShowPassword value to the password editor
            LogonParametersHelper.ApplyShowPasswordToEditor(View);
        }
    }
}