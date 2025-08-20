using DemoScheduler3.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoScheduler3.Module.Controllers
{
    public class SignatureController : ViewController
    {
        public SignatureController() : base()
        {

            PopupWindowShowAction showSingletonAction =
                new PopupWindowShowAction(this, "Sign document", PredefinedCategory.View);
            showSingletonAction.CustomizePopupWindowParams += showSingletonAction_CustomizePopupWindowParams;
            showSingletonAction.Execute += showSingletonAction_Execute;

            this.TargetObjectType = typeof(Document);
            this.TargetViewType = ViewType.DetailView;

        }
        private void showSingletonAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(CustomLogonParameters));
            var clp = objectSpace.CreateObject<CustomLogonParameters>();
            DetailView detailView = Application.CreateDetailView(objectSpace, clp);
            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }
        void showSingletonAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var Parameters = e.PopupWindowViewCurrentObject as CustomLogonParameters;
            var os = this.Application.CreateObjectSpace<ApplicationUser>();
            var SigningUser = os.GetObjectsQuery<ApplicationUser>().FirstOrDefault(u => u.UserName == Parameters.UserName);
       
            if(SigningUser!= null)
            {
               if(SigningUser.ComparePassword(Parameters.Password))
                {
                    var signature = os.CreateObject<DocumentSignature>();
                    signature.User = SigningUser;
                    signature.SignatureDate = DateTime.Now;
                    signature.Document = os.GetObject<Document>(this.View.CurrentObject as Document);
                    os.CommitChanges();
                }
             

            }
            else
            {
                throw new UserFriendlyException("User not found. Please check the username.");
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
    }
}
