using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationCAD.Model;
using StationCAD.Model.Notifications.Mailgun;
using StationCAD.Processor.Notifications;
using System.IO;

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
                string data;
                using (StreamReader sr = new StreamReader(@"TestData\ASCIITemp85.txt"))
                { data = sr.ReadToEnd(); }
                EmailNotification email = new EmailNotification
                {
                    Recipient = "ccpafd47@stationcad.graphitegear.com",
                    OrganizationName = "Lionville Fire Company",
                    MessageSubject = "Incident - AFA",
                    MessageBody = data
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
