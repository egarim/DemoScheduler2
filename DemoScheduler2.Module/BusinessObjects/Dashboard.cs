using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace DemoScheduler2.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Dashboard")]
    public class Dashboard : BaseObject
    {
        public Dashboard(Session session) : base(session)
        {
        }

        private string _name;
        [Size(255)]
        public string Name
        {
            get => _name;
            set => SetPropertyValue(nameof(Name), ref _name, value);
        }

        private string _description;
        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get => _description;
            set => SetPropertyValue(nameof(Description), ref _description, value);
        }

        [Association("Dashboard-GridEvents")]
        //[DevExpress.ExpressApp.Model.ModelDefault("ListView", "Dashboard_GridEvents_ListView")]
        public XPCollection<MyEvent> GridEvents => GetCollection<MyEvent>(nameof(GridEvents));

        [Association("Dashboard-SchedulerEvents")]
        //[DevExpress.ExpressApp.Model.ModelDefault("ListView", "Dashboard_SchedulerEvents_ListView")]
        public XPCollection<MyEvent> SchedulerEvents => GetCollection<MyEvent>(nameof(SchedulerEvents));

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Initialize with default values if needed
        }
    }
}