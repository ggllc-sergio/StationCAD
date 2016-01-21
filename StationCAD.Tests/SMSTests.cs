using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationCAD.Model;
using StationCAD.Model.Helpers;
using StationCAD.Model.Clickatell;
using StationCAD.Processor;

namespace StationCAD.Tests
{
    [TestClass]
    public class SMSTests
    {
        [TestMethod]
        public void CreateSMSNotificationJsonTest()
        {
            SMSNotification sms = new SMSNotification();
            sms.IncidentNumber = "LNVF2016000293";
            sms.DispatchTime = DateTime.Now.ToString("MM/dd/yy");
            sms.Location = "15 S Village Ave";
            sms.Municipality = "Uwchlan";
            sms.Notes = "Oh mah-god!!!  The building is on.... fi-er.";
            sms.TypeCode = "Building Fire";
            sms.Recipients = new List<string>();
            sms.Recipients.Add("16108833253");
            sms.Recipients.Add("16108833254");
            sms.Recipients.Add("14849958731");

            string json = JsonUtil<SMSNotification>.ToJson(sms);
            Console.WriteLine(json);
        }

        [TestMethod]
        public void CreateSMSNotificationTest()
        {
            CreateSMSNotification().Wait();
        }

        protected async Task CreateSMSNotification()
        {

            SMSNotification sms = new SMSNotification();
            sms.IncidentNumber = "LNVF2016000293";
            sms.DispatchTime = DateTime.Now.ToString("MM/dd/yy");
            sms.Location = "15 S Village Ave";
            sms.Municipality = "Uwchlan";
            sms.Notes = "Oh mah-god!!!  The building is on.... fi-er.";
            sms.TypeCode = "Building Fire";
            sms.Recipients = new List<string>();
            sms.Recipients.Add("16108833253");
            sms.Recipients.Add("16108833254");
            sms.Recipients.Add("14849958731");

            try
            {
                SMSNotifications smsNotifier = new SMSNotifications();
                string result = await smsNotifier.CreateNotification(sms);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); }
        }
    }
}
