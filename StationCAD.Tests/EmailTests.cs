using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationCAD.Model;
using StationCAD.Model.DataContexts;
using StationCAD.Model.Notifications.Mailgun;
using StationCAD.Processor.Notifications;
using System.IO;
using System.Linq;

namespace StationCAD.Tests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void EmailAPITest()
        {
            try
            {
                string data;
                using (StreamReader sr = new StreamReader(@"TestData\ASCIITemp85.txt"))
                { data = sr.ReadToEnd(); }
                EmailNotification email = new EmailNotification
                {
                    Recipient = "skip513@gmail.com",
                    //Recipient = "ccpafd47@stationcad.graphitegear.com",
                    OrganizationName = "Lionville Fire Company",
                    MessageSubject = "Incident - AFA",
                    MessageBody = data
                };
                string result = Email.SendEmailMessage(email);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        [TestMethod]
        public void SMSEmailAPITest()
        {
            try
            {
                SMSEmailNotification email = new SMSEmailNotification
                {
                    MobileNumber = "6108833253",
                    Carrier = MobileCarrier.ATT,
                    OrganizationName = "Lionville Fire Company",
                    MessageSubject = "Incident - AFA",
                    MessageBody = "Fire Alarm \r\n 15 S Village Ave"
                };
                string result = Email.SendEmailMessage(email);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        [TestMethod]
        public void EmailAPINotificationTest()
        {

            using (var db = new StationCADDb())
            {
                Incident inc = db.Incidents
                    .Include("Organization")
                    .Include("LocationAddresses")
                    .Include("Notes")
                    .Include("Units")
                    .Where(x => x.LocalIncidentID == "F16001462").FirstOrDefault();
                if (inc != null)
                {
                    UserProfile usr;
                    usr = db.UserProfiles
                        .Include("OrganizationAffiliations")
                        .Include("MobileDevices")
                        .Where(w => w.NotificationEmail == "skip513@gmail.com")
                        .FirstOrDefault();
                    if (usr == null)
                    {
                        usr = new UserProfile();
                        usr.FirstName = string.Format("FirstName_{0}", DateTime.Now.Ticks);
                        usr.LastName = string.Format("LastName_{0}", DateTime.Now.Ticks);
                        usr.IdentificationNumber = DateTime.Now.Ticks.ToString();
                        //usr.UserName = string.Format("{0}.{1}", usr.FirstName, usr.LastName);
                        usr.OrganizationAffiliations = new List<OrganizationUserAffiliation>();
                        usr.OrganizationAffiliations.Add(new OrganizationUserAffiliation { Status = OrganizationUserStatus.Active, Role = OrganizationUserRole.User });
                        usr.NotificationEmail = "skip513@gmail.com";
                        usr.MobileDevices = new List<UserMobileDevice>();
                        usr.MobileDevices.Add(new UserMobileDevice { Carrier = MobileCarrier.ATT, EnableSMS = true, MobileNumber = "6108833253" });

                        db.UserProfiles.Add(usr);
                        db.SaveChanges();
                    }
                    EmailNotification email = inc.GetEmailNotification(usr.OrganizationAffiliations.First());
                    string emailResult = Email.SendEmailMessage(email);
                    SMSEmailNotification sms = inc.GetSMSEmailNotification(usr.OrganizationAffiliations.First());
                    string smsResult = Email.SendEmailMessage(sms);
                    Console.WriteLine(string.Format("Email: {0}; SMS: {1}", emailResult, smsResult));

                    db.UserProfiles.Remove(usr);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Couldn't find it.");
                }
            }
        }
    }
}
