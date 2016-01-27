using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StationCAD.Model.Notifications.Mailgun;
using StationCAD.Processor.Notifications;

namespace StationCAD.Tests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void EmailAPITest()
        {
            Email emailer = new Email();
            try
            {
                EmailNotification email = new EmailNotification
                {
                    Recipient = "skip513@gmail.com",
                    OrganizationName = "Lionville Fire Company",
                    MessageSubject = "Incident - AFA",
                    MessageBody = "Fire Alarm \r\n 15 S Village Ave"
                };
                string result = emailer.SendEmailMessage(email);
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
            Email emailer = new Email();
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
                string result = emailer.SendEmailMessage(email);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
