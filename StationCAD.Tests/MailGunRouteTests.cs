using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationCAD.Processor.Notifications;

namespace StationCAD.Tests
{
    [TestClass]
    public class MailGunRouteTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Email.CreateRoute("Test One", "CC47-LVFC");
        }
    }
}
