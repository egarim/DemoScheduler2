using System.ComponentModel;
using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DemoScheduler2.Module.BusinessObjects;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;

namespace DemoScheduler2.Module {
    public static class LogonParametersHelper {
        // This method can be called from a controller or module to apply the ShowPassword value to UI elements
        public static void ApplyShowPasswordToEditor(DetailView detailView) {
            if (detailView != null && detailView.ViewEditMode == ViewEditMode.Edit) {
                var logonParameters = detailView.CurrentObject as CustomLogonParameters;
                if (logonParameters != null) {
                    var passwordEditor = detailView.FindItem("Password") as PropertyEditor;
                    if (passwordEditor != null) {
                        // Get the underlying editor control
                        var editorControl = passwordEditor.Control;
                        
                        // Set the PasswordChar property based on ShowPassword value
                        if (editorControl != null) {
                            // Here we use reflection to set the appropriate property
                            // This is a generic approach as the actual control type differs by platform
                            try {
                                PropertyInfo passwordCharProperty = editorControl.GetType().GetProperty("PasswordChar");
                                if (passwordCharProperty != null) {
                                    char passwordChar = logonParameters.ShowPassword ? '\0' : '*';
                                    passwordCharProperty.SetValue(editorControl, passwordChar);
                                }
                                
                                // For DevExpress editors that use Properties
                                PropertyInfo propertiesInfo = editorControl.GetType().GetProperty("Properties");
                                if (propertiesInfo != null) {
                                    object properties = propertiesInfo.GetValue(editorControl);
                                    if (properties != null) {
                                        PropertyInfo usePasswordInfo = properties.GetType().GetProperty("UseSystemPasswordChar");
                                        if (usePasswordInfo != null) {
                                            usePasswordInfo.SetValue(properties, !logonParameters.ShowPassword);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex) {
                                Tracing.Tracer.LogError(ex);
                            }
                        }
                    }
                }
            }
        }
    }
}