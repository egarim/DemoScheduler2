using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using Microsoft.Extensions.DependencyInjection;
using DemoScheduler2.Module.BusinessObjects;

namespace DemoScheduler2.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        
        // Create sample Dashboard if it doesn't exist
        Dashboard sampleDashboard = ObjectSpace.FirstOrDefault<Dashboard>(d => d.Name == "Sample Dashboard");
        if(sampleDashboard == null) {
            sampleDashboard = ObjectSpace.CreateObject<Dashboard>();
            sampleDashboard.Name = "Sample Dashboard";
            sampleDashboard.Description = "A sample dashboard with grid and scheduler events";

            // Create some sample events for the grid
            for(int i = 1; i <= 3; i++) {
                var gridEvent = ObjectSpace.CreateObject<BusinessObjects.MyEvent>();
                gridEvent.Subject = $"Grid Event {i}";
                gridEvent.StartOn = DateTime.Today.AddDays(i);
                gridEvent.EndOn = DateTime.Today.AddDays(i).AddHours(2);
                gridEvent.GridDashboard = sampleDashboard;
            }

            // Create some sample events for the scheduler
            for(int i = 1; i <= 3; i++) {
                var schedulerEvent = ObjectSpace.CreateObject<BusinessObjects.MyEvent>();
                schedulerEvent.Subject = $"Scheduler Event {i}";
                schedulerEvent.StartOn = DateTime.Today.AddDays(i + 7);
                schedulerEvent.EndOn = DateTime.Today.AddDays(i + 7).AddHours(1);
                schedulerEvent.SchedulerDashboard = sampleDashboard;
            }

            ObjectSpace.CommitChanges();
        }
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
        //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
}
