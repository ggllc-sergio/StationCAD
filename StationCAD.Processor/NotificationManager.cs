using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StationCAD.Model.Notifications.OneSignal;
using StationCAD.Model.Notifications.Mailgun;
using StationCAD.Model.Notifications.Clickatell;
using StationCAD.Model;
using StationCAD.Model.DataContexts;

namespace StationCAD.Processor
{
    public class NotificationManager
    {
        public List<OrganizationUserNotifcation> CreateNotifications(Incident incident)
        {
            List<OrganizationUserNotifcation> results = new List<OrganizationUserNotifcation>();
            using (var db = new StationCADDb())
            {
                List<UserOrganizationAffiliation> users = db.UserOrganizationAffiliations.Where(x => x.OrganizationId == incident.OrganizationId).ToList();
                foreach(UserOrganizationAffiliation user in users)
                {

                }
            }
            return new List<OrganizationUserNotifcation>();
        }

        public void NotifyUsers(List<OrganizationUserNotifcation> users)
        {

            // First group the notifications by notificaion Type
            List<NotificationGroup> groups = users
                .GroupBy(g => g.NotifcationType)
                .Select(x => new NotificationGroup { Type = x.Key, Users = x.ToList<OrganizationUserNotifcation>() })
                .ToList<NotificationGroup>();
            // Use TPL to process each type of notification
            if (groups.Count> 0)
            {
                ParallelOptions tplOptions = new ParallelOptions();
                tplOptions.MaxDegreeOfParallelism = ParallelismFactor;
                Parallel.ForEach<NotificationGroup>(groups, x => ProcessNotificationGroup(x));
            }
        }
        

        #region Methods for sending notificaions 

        #endregion

        #region TPL task for notification processing 

        private void ProcessNotificationGroup(NotificationGroup group)
        {
            switch(group.Type)
            {
                case OrganizationUserNotifcationType.Email:

                    break;
                case OrganizationUserNotifcationType.TextMessage:

                    break;

                default:


                    break;
            }
        }
        #endregion


        protected int ParallelismFactor
        {
            get
            {
                int dop = 1;
                int procCnt = Environment.ProcessorCount;
                // if the available procs are 4 or more, use half.
                // otherwise just use 1.
                if (procCnt > 3)
                { dop = procCnt / 2; }
                else
                { dop = 1; }

                return dop;
            }
        }
    }

    public class NotificationGroup
    {
        public OrganizationUserNotifcationType Type { get; set; }
        public List<OrganizationUserNotifcation> Users { get; set; }
    }
}
