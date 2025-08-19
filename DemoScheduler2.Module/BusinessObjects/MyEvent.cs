using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace DemoScheduler2.Module.BusinessObjects
{
    public class MyEvent : DevExpress.Persistent.BaseImpl.Event
    {
        public MyEvent(Session session) : base(session)
        {
        }

        private Dashboard _gridDashboard;
        [Association("Dashboard-GridEvents")]
        public Dashboard GridDashboard
        {
            get => _gridDashboard;
            set => SetPropertyValue(nameof(GridDashboard), ref _gridDashboard, value);
        }

        private Dashboard _schedulerDashboard;
        [Association("Dashboard-SchedulerEvents")]
        public Dashboard SchedulerDashboard
        {
            get => _schedulerDashboard;
            set => SetPropertyValue(nameof(SchedulerDashboard), ref _schedulerDashboard, value);
        }
    }
}