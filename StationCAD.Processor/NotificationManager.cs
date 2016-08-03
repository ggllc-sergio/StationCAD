using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StationCAD.Model.Notifications.OneSignal;
using StationCAD.Model.Notifications.Mailgun;
using StationCAD.Model.Notifications.Clickatell;
using StationCAD.Model;
using StationCAD.Model.DataContexts;
using System.Collections.Concurrent;
using StationCAD.Processor.Notifications;
using System.Configuration;

namespace StationCAD.Processor
{
    public static class NotificationManager
    {
        public static List<OrganizationUserNotifcation> CreateNotifications(Incident incident)
        {
            List<OrganizationUserNotifcation> results = new List<OrganizationUserNotifcation>();
            ConcurrentBag<OrganizationUserNotifcation> resultsBag = new ConcurrentBag<OrganizationUserNotifcation>();
            List<UserOrganizationAffiliation> uoas;
            using (var db = new StationCADDb())
            {
                uoas = db.UserOrganizationAffiliations
                    .Include("CurrentUser")
                    .Include("CurrentOrganization")
                    .Where(x => x.OrganizationId == incident.OrganizationId).ToList();
            }
            if (uoas == null)
                throw new InvalidProgramException("Unable to find valid User-Org Affiliations.");

            ParallelOptions opts = new ParallelOptions();
            opts.MaxDegreeOfParallelism = ParallelismFactor;
            ParallelLoopResult ptlseResult = Parallel.ForEach(
                uoas,
                opts,
                current =>
                {
                    OrganizationUserNotifcation item = new OrganizationUserNotifcation();
                    SMSEmailNotification notification = incident.GetSMSEmailNotification(current.CurrentUser);
                    item.NotifcationType = OrganizationUserNotifcationType.TextMessage;
                    item.Notification = notification;
                    item.MessageTitle = notification.MessageSubject;
                    item.MessageBody = notification.MessageBody;
                    item.UserOrganizationAffiliationId = current.Id;
                    resultsBag.Add(item);
                });
                
            ParallelLoopResult ptleResult = Parallel.ForEach(
                uoas,
                opts,
                current =>
                {
                    OrganizationUserNotifcation item = new OrganizationUserNotifcation();
                    EmailNotification notification = incident.GetEmailNotification(current.CurrentUser);
                    item.NotifcationType = OrganizationUserNotifcationType.Email;
                    item.Notification = notification;
                    item.MessageTitle = notification.MessageSubject;
                    item.MessageBody = notification.MessageBody;
                    item.UserOrganizationAffiliationId = current.Id;
                    resultsBag.Add(item);
                });
            return resultsBag.ToList<OrganizationUserNotifcation>();
        }

        public static void NotifyUsers(ref List<OrganizationUserNotifcation> users)
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
                Parallel.ForEach<NotificationGroup>(groups, x => ProcessNotificationGroup(ref x));
            }
        }
        

        #region Methods for sending notificaions 

        #endregion

        #region TPL task for notification processing 

        private static void ProcessNotificationGroup(ref NotificationGroup group)
        {
            switch(group.Type)
            {
                case OrganizationUserNotifcationType.Email:
                    foreach(var item in group.Users)
                    {
                        string result = Email.SendEmailMessage(item.Notification as EmailNotification);
                        if (result == "OK")
                            item.Sent = DateTime.Now;
                    }
                    break;
                case OrganizationUserNotifcationType.TextMessage:

                    bool enableSMSGateway;
                    bool.TryParse(ConfigurationManager.AppSettings["enableSMSGateway"], out enableSMSGateway);
                    foreach (var item in group.Users)
                    {
                        if(enableSMSGateway)
                        {

                        }
                        else
                        {
                            string result = Email.SendEmailMessage(item.Notification as SMSEmailNotification);
                            if (result == "OK")
                                item.Sent = DateTime.Now;
                        }
                    }
                    break;

                default:


                    break;
            }
        }
        #endregion


        private static int ParallelismFactor
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
