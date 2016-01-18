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
    }
}
