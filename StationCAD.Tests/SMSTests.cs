using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StationCAD.Model.Helpers;
using StationCAD.Model.Notifications.Clickatell;
using StationCAD.Processor.Notifications;

namespace StationCAD.Tests
{
    [TestClass]
    public class SMSTests
    {
        [TestMethod]
        public void CreateSMSNotificationJsonTest()
        {
            SMSNotification sms = new SMSNotification();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("LNVF2016000293");
            sb.AppendLine(DateTime.Now.ToString("MM/dd/yy"));
            sb.AppendLine("15 S Village Ave");
            sb.AppendLine("Uwchlan");
            sb.AppendLine("Oh mah-god!!!  The building is on.... fi-er.");
            sb.AppendLine("Building Fire");
            sms.Text = sb.ToString();
            sms.Recipients = new List<string>();
            sms.Recipients.Add("16108833253");
            //sms.Recipients.Add("16108833254");
            //sms.Recipients.Add("14849958731");

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
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("LNVF2016000293");
            sb.AppendLine(DateTime.Now.ToString("MM/dd/yy"));
            sb.AppendLine("15 S Village Ave");
            sb.AppendLine("Uwchlan");
            sb.AppendLine("Oh mah-god!!!  The building is on.... fi-er.");
            sb.AppendLine("Building Fire");
            sms.Text = sb.ToString();
            sms.Recipients = new List<string>();
            sms.Recipients.Add("16108833253");
            //sms.Recipients.Add("16108833254");
            //sms.Recipients.Add("14849958731");

            try
            {
                SMS smsNotifier = new SMS();
                string result = await smsNotifier.CreateNotification(sms);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); }
        }
    }
}
