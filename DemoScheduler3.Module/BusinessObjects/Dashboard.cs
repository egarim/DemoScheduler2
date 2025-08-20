using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace DemoScheduler3.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Dashboard")]
    [RuleObjectExists("AnotherDashboardExists", DefaultContexts.Save, "True", InvertResult = true,
        CustomMessageTemplate = "Another Dashboard already exists.")]
    [RuleCriteria("CannotDeleteDashboard", DefaultContexts.Delete, "False",
        CustomMessageTemplate = "Cannot delete Dashboard.")]
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
        public XPCollection<Event> AllEvents;

        void InitEvents()
        {
            if(AllEvents == null)
            {
                AllEvents = new XPCollection<Event>(Session);
                AllEvents.Criteria = CriteriaOperator.FromLambda<Event>(e => e.StartOn.Date==DateTime.Today.Date);
            }
        }
     
        public XPCollection<Event> GridEvents
        {
            get
            {
                InitEvents();
                return AllEvents;
            }

        }


        //[DevExpress.ExpressApp.Model.ModelDefault("ListView", "Dashboard_SchedulerEvents_ListView")]
        public XPCollection<Event> SchedulerEvents
        {
            get
            {
                InitEvents();
                return AllEvents;
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Initialize with default values if needed
        }
    }
}