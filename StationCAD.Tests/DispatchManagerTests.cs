using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using StationCAD.Model;
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
            string data;
            using (StreamReader sr = new StreamReader(@"TestData\Test-Dispatch-CHESCO-1.txt"))
            { data = sr.ReadToEnd(); }
            DispatchManager<ChesCoPAEventMessage> dispMgr = new DispatchManager<ChesCoPAEventMessage>();
            //DispatchEvent eventMsg = dispMgr.ParseEventText(data);
            dispMgr.ProcessEvent(data);
            //string json = JsonUtil<DispatchEvent>.ToJson(eventMsg);
            //Console.WriteLine(json);

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
