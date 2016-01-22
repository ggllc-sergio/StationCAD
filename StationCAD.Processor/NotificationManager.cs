using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationCAD.Model.Notifications.OneSignal;
using StationCAD.Model.Notifications.Mailgun;
using StationCAD.Model.Notifications.Clickatell;
using StationCAD.Model;

namespace StationCAD.Processor
{
    public class NotificationManager
    {

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

        #region Type based notification creation 

        protected SMSNotification CreateSMSNotification(OrganizationUserNotifcation notification)
        {
            return new SMSNotification();
        }

        protected EmailNotification CreateEmailNotification(OrganizationUserNotifcation notification)
        {
            return new EmailNotification();
        }

        protected PushNotification CreatePushNotification(OrganizationUserNotifcation notification)
        {
            return new PushNotification();
        }

        #endregion

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
