using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationCAD.Model;
using StationCAD.Model.Helpers;
using StationCAD.Model.OneSignal;
using StationCAD.Processor;

namespace StationCAD.Tests
{
    [TestClass]
    public class ProcessorTests
    {
        [TestMethod]
        public void GetPlayersTest()
        {
            GetPlayers().Wait();
        }


        [TestMethod]
        public void GetPlayerTest()
        {
            GetPlayer().Wait();
        }


        [TestMethod]
        public void GetNotificationsTest()
        {
            GetNotifications().Wait();
        }


        [TestMethod]
        public void GetNotificationTest()
        {
            GetNotification().Wait();
        }


        [TestMethod]
        public void CreateNotificationJsonTest()
        {
            PushNotificationCreate push = new PushNotificationCreate();
            push.ApplicationId = "950a66d5-c457-4c9e-9a8b-cca7ef8b8c68";
            push.Contents = new Dictionary<string, string>(); 
            push.Contents.Add("en", "Server API Test Mesage");
            push.IncludeSegments = new List<string> { "All" };
            push.SendiOS = false;

            try {
                string json = JsonUtil<PushNotificationCreate>.ToJson(push);
                Console.WriteLine(json);
            }
            catch(Exception ex)
            { Console.WriteLine(ex.ToString()); }
        }


        [TestMethod]
        public void CreateNotificationTest()
        {
            CreateNotification().Wait();           
        }

        #region Protected Methods 

        protected async Task GetPlayer()
        {
            User player = new User { IdentificationNumber = "5605d53f-9d13-4865-a23c-26dfd92d5c4a" };
            
            PushNotifications pushNotifier = new PushNotifications();
            Device result = await pushNotifier.GetDevice(player);
            string json = JsonUtil<Device>.ToJson(result);
            Console.WriteLine(json);
        }

        protected async Task GetPlayers()
        {
            PushNotifications pushNotifier = new PushNotifications();
            DeviceList result = await pushNotifier.GetDevices();
            string json = JsonUtil<DeviceList>.ToJson(result);
            Console.WriteLine(json);
        }

        protected async Task GetNotifications()
        {
            PushNotifications pushNotifier = new PushNotifications();
            PushNotificationList result = await pushNotifier.GetNotifications();
            string json = JsonUtil<PushNotificationList>.ToJson(result);
            Console.WriteLine(json);
        }

        protected async Task GetNotification()
        {
            PushNotifications pushNotifier = new PushNotifications();
            PushNotification result = await pushNotifier.GetNotification("bee2a35c-9f5d-425d-b4ff-6196e033efc0");
            string json = JsonUtil<PushNotification>.ToJson(result);
            Console.WriteLine(json);
        }

        protected async Task CreateNotification()
        {
            PushNotificationCreate push = new PushNotificationCreate();
            push.Contents = new Dictionary<string, string>();
            push.Contents.Add("en", "Server API Test Mesage");
            push.IncludeSegments = new List<string> { "All" };
            push.SendiOS = false;

            try
            {
                PushNotifications pushNotifier = new PushNotifications();
                string result = await pushNotifier.CreatNotification(push);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); }
        }
        #endregion

    }
}
