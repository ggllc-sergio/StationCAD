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
using NLog;

namespace StationCAD.Processor
{
    public static class NotificationManager
    {
        public static List<OrganizationUserNotification> CreateNotifications(Incident incident)
        {
            List<OrganizationUserNotification> results = new List<OrganizationUserNotification>();
            ConcurrentBag<OrganizationUserNotification> resultsBag = new ConcurrentBag<OrganizationUserNotification>();
            List<OrganizationUserAffiliation> uoas;
            List<OrganizationUserNotification> notifications;
            try
            {
                using (var db = new StationCADDb())
                {
                    uoas = db.OrganizationUserAffiliations
                        .Include("CurrentUserProfile")
                        .Include("CurrentUserProfile.MobileDevices")
                        .Include("CurrentOrganization")
                        .Where(x => x.CurrentOrganization.Id == incident.OrganizationId && x.Status == OrganizationUserStatus.Active).ToList();

                    if (uoas == null)
                        throw new InvalidProgramException("Unable to find valid UserProfile-Org Affiliations.");

                    ParallelOptions opts = new ParallelOptions();
                    opts.MaxDegreeOfParallelism = ParallelismFactor;
                    ParallelLoopResult ptlseResult = Parallel.ForEach(
                        uoas,
                        opts,
                        current =>
                        {
                            OrganizationUserNotification item = new OrganizationUserNotification();
                            SMSEmailNotification notification = incident.GetSMSEmailNotification(current);
                            item.NotifcationType = OrganizationUserNotifcationType.TextMessage;
                            item.Notification = notification;
                            item.MessageTitle = notification.MessageSubject;
                            item.MessageBody = notification.MessageBody;
                            item.Affilitation = current;
                            resultsBag.Add(item);
                        });

                    ParallelLoopResult ptleResult = Parallel.ForEach(
                        uoas,
                        opts,
                        current =>
                        {
                            OrganizationUserNotification item = new OrganizationUserNotification();
                            EmailNotification notification = incident.GetEmailNotification(current);
                            item.NotifcationType = OrganizationUserNotifcationType.Email;
                            item.Notification = notification;
                            item.MessageTitle = notification.MessageSubject;
                            item.MessageBody = notification.MessageBody;
                            item.Affilitation = current;
                            resultsBag.Add(item);
                        });
                    notifications = resultsBag.ToList<OrganizationUserNotification>();
                    db.OrganizationUserNotifcations.AddRange(notifications);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("An error occurred in NotificationManager.CreateNotifications(). Exception: {0}", ex.Message);
                LogException(errMsg, ex);
                throw ex;
            }
            return notifications;
        }

        public static void NotifyUsers(ref List<OrganizationUserNotification> users)
        {
            try
            {
                // First group the notifications by notificaion Type
                List<NotificationGroup> groups = users
                    .GroupBy(g => g.NotifcationType)
                    .Select(x => new NotificationGroup { Type = x.Key, Users = x.ToList<OrganizationUserNotification>() })
                    .ToList<NotificationGroup>();
                // Use TPL to process each type of notification
                if (groups.Count > 0)
                {
                    ParallelOptions tplOptions = new ParallelOptions();
                    tplOptions.MaxDegreeOfParallelism = ParallelismFactor;
                    Parallel.ForEach<NotificationGroup>(groups, x => ProcessNotificationGroup(ref x));
                }
                using (var db = new StationCADDb())
                {
                    foreach (OrganizationUserNotification item in users)
                    { db.OrganizationUserNotifcations.Attach(item); }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("An error occurred in NotificationManager.NotifyUsers(). Exception: {0}", ex.Message);
                LogException(errMsg, ex);
                throw ex;
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
        
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static void LogInfo(string message)
        {
            logger.Log(LogLevel.Info, message);
        }

        private static void LogException(string message, Exception ex)
        {
            logger.Log(LogLevel.Error, ex, message);
        }

    }

    public class NotificationGroup
    {
        public OrganizationUserNotifcationType Type { get; set; }
        public List<OrganizationUserNotification> Users { get; set; }
    }
}
