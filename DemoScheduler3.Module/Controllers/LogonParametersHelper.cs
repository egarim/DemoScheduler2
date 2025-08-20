using DemoScheduler3.Module.BusinessObjects;
using System.ComponentModel;
using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;

namespace DemoScheduler3.Module.Controllers
{
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

                        passwordEditor.IsPassword = logonParameters.ShowPassword;
                      
                    }
                }
                detailView.SaveModel();
            }
        }
    }
}