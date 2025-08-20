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
using static System.Net.Mime.MediaTypeNames;

namespace DemoScheduler3.Module.Controllers
{
    public class ShowSingletonController : WindowController
    {
        public ShowSingletonController()
        {
this.TargetWindowType = WindowType.Main;
            PopupWindowShowAction showSingletonAction =
                new PopupWindowShowAction(this, "Show scheduler dashboard", PredefinedCategory.View);
            showSingletonAction.CustomizePopupWindowParams += showSingletonAction_CustomizePopupWindowParams;            
        }
        private void showSingletonAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Dashboard));
            DetailView detailView = Application.CreateDetailView(objectSpace, objectSpace.GetObjects<Dashboard>()[0]);
            detailView.ViewEditMode = ViewEditMode.Edit;
            e.View = detailView;
        }
    }
}