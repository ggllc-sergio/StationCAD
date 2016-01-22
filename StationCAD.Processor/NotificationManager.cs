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

        public void NotifyUsers()
        {

        }

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
    }
}
