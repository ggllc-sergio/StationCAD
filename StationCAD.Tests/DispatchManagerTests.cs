using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using StationCAD.Model;
using StationCAD.Model.DataContexts;
using StationCAD.Processor;
using System.IO;
using StationCAD.Model.Helpers;

namespace StationCAD.Tests
{
    /// <summary>
    /// Summary description for DispatchManagerTests
    /// </summary>
    [TestClass]
    public class DispatchManagerTests
    {
        public DispatchManagerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestEventParsing()
        {
            string tag = "CC47-LVFC";
            using (var db = new StationCADDb())
            {
                string data;
                using (StreamReader sr = new StreamReader(@"TestData\Test-Dispatch-CHESCO-2.txt"))
                { data = sr.ReadToEnd(); }
                DispatchManager<ChesCoPAEventMessage> dispMgr = new DispatchManager<ChesCoPAEventMessage>();

                Organization org = db.Organizations.Where(x => x.Tag == tag).FirstOrDefault();
                if (org == null)
                {
                    org = new Organization();
                    org.Name = "Lionville Volunteer Fire Company";
                    org.Status = OrganizationStatus.Active;
                    org.Type = OrganizationType.Fire;
                    org.Tag = tag;
                    org.ContactEmail = "sergio.ora@graphitegear.com";
                    org.ContactPhone = "610.883.3253";
                    //OrganizationAddress addr = new OrganizationAddress();
                    //addr.Type = AddressType.MainStation;
                    //addr.Number = "15";
                    //addr.Street = "Village Ave";
                    //addr.City = "Lionville";
                    //addr.Municipality = "Uwchlan";
                    //addr.County = "Chester";
                    //addr.State = "PA";
                    //addr.PostalCode = "19353";
                    //addr.BillingAddress = true;
                    //addr.MailingAddress = true;
                    //addr.PrimaryBilling = true;
                    //addr.PrimaryMailing = true;

                    //org.Addresses = new List<OrganizationAddress>();
                    //org.Addresses.Add(addr);
                    //org.BillingAddress = addr;
                    //org.MailingAddress = addr;
                    
                    db.Organizations.Add(org);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }

                //DispatchEvent eventMsg = dispMgr.ParseEventText(data);
                dispMgr.ProcessEvent(org, data);
                //string json = JsonUtil<DispatchEvent>.ToJson(eventMsg);
                //Console.WriteLine(json);
            }
        }

        [TestMethod]
        public void ParseDate()
        {
            string dt = "28-07-2016              13:42";
            string[] dtParts = dt.Split(' ');
            List<string> dtTrimmed = new List<string>();
            foreach(string item in dtParts)
            {
                if (item != string.Empty)
                    dtTrimmed.Add(item);
            }
            dtParts = dtTrimmed.ToArray();
            string[] dParts = dtParts[0].Split('-');
            string[] tParts = dtParts[1].Split(':');
            DateTime dtParsed = new DateTime(int.Parse(dParts[2]), int.Parse(dParts[1]), int.Parse(dParts[0]), int.Parse(tParts[0]), int.Parse(tParts[1]), 0);

            Console.WriteLine(dtParsed.ToString());
        }
    }
}
