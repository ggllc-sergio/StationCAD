using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationCAD.Model;
using StationCAD.Model.Helpers;
using StationCAD.Processor;

namespace StationCAD.Tests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void EmailAPITest()
        {
            EmailNotifications emailer = new EmailNotifications();
            try
            {
                EmailNotification email = new EmailNotification
                {
                    Recipient = "skip513@gmail.com",
                    OrganizationName = "Lionville Fire Company",
                    MessageSubject = "Incident - AFA",
                    MessageBody = "Fire Alarm \r\n 15 S Village Ave"
                };
                string result = emailer.SendAPIMessage(email);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
