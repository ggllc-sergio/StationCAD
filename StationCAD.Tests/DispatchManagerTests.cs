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
using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        public void TestDispatchEventParsing()
        {
            string tag = "CC51-FWFC";
            using (var db = new StationCADDb())
            {

                string data;
                using (StreamReader sr = new StreamReader(@"TestData\UnitDispatchReport-F16001716.htm"))
                { data = sr.ReadToEnd(); }
                DispatchManager dispMgr = new DispatchManager();

                Organization org = db.Organizations.Where(x => x.Tag == tag).FirstOrDefault();
                if (org == null)
                {
                    org = new Organization();
                    org.Name = "First West Chester Fire Company";
                    org.Status = OrganizationStatus.Active;
                    org.Type = OrganizationType.Fire;
                    org.Tag = tag;
                    org.ContactEmail = "sergio.ora@graphitegear.com";
                    org.ContactPhone = "610.883.3253";

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
                // Add UserProfile
                string email = "skip513@gmail.com";
                User user;
                UserProfile usrp;
                user = db.Users
                    .Include("Profile")
                    .Include("Profile.OrganizationAffiliations")
                    .Include("Profile.MobileDevices")
                    .Where(w => w.Email == email)
                    .FirstOrDefault();
                if (user == null)
                {
                    user = new User { Id = Guid.NewGuid().ToString(), UserName = email, Email = email };
                    usrp = new UserProfile();
                    usrp.FirstName = string.Format("FirstName_{0}", DateTime.Now.Ticks);
                    usrp.LastName = string.Format("LastName_{0}", DateTime.Now.Ticks);
                    usrp.AccountEmail = email;                    usrp.IdentificationNumber = DateTime.Now.Ticks.ToString();
                    //usrp.UserName = string.Format("{0}.{1}", usrp.FirstName, usrp.LastName);
                    usrp.OrganizationAffiliations = new List<OrganizationUserAffiliation>();
                    usrp.OrganizationAffiliations.Add(new OrganizationUserAffiliation { Status = OrganizationUserStatus.Active, Role = OrganizationUserRole.User });
                    usrp.NotificationEmail = email;
                    usrp.MobileDevices = new List<UserMobileDevice>();
                    usrp.MobileDevices.Add(new UserMobileDevice { Carrier = MobileCarrier.ATT, EnableSMS = true, MobileNumber = "6108833253" });
                    user.Profile = usrp;
                    user.Profile.OrganizationAffiliations = new List<OrganizationUserAffiliation>();
                    OrganizationUserAffiliation uoa = new OrganizationUserAffiliation();
                    uoa.CurrentOrganization = org;
                    uoa.Role = OrganizationUserRole.User;
                    uoa.Status = OrganizationUserStatus.Active;
                    
                    user.Profile.OrganizationAffiliations.Add(uoa);
                    db.Users.Add(user);
                }

                //usr2 = db.UserProfiles
                //    .Include("OrganizationAffiliations")b
                //    .Include("MobileDevices")
                //    .Where(w => w.NotificationEmail == "firemike45@gmail.com")
                //    .FirstOrDefault();
                //if (usr2 == null)
                //{ 
                //    usr2 = new UserProfile();
                //    usr2.FirstName = "Michael";
                //    usr2.LastName = "Lam";
                //    usr2.IdentificationNumber = DateTime.Now.Ticks.ToString();
                ////    usr2.UserName = string.Format("{0}.{1}", usr2.FirstName, usr2.LastName);
                //    usr2.OrganizationAffiliations = new List<UserOrganizationAffiliation>();
                //    usr2.OrganizationAffiliations.Add(new UserOrganizationAffiliation { Status = OrganizationUserStatus.Active, Role = OrganizationUserRole.User });
                //    usr2.NotificationEmail = "firemike45@gmail.com";
                //    usr2.MobileDevices = new List<UserMobileDevice>();
                //    usr2.MobileDevices.Add(new UserMobileDevice { Carrier = MobileCarrier.ATT, EnableSMS = true, MobileNumber = "6108833253" });
                //    db.UserProfiles.Add(usr2);
                //}
                db.SaveChanges();
                dispMgr.ProcessEvent(org, data, DispatchManager.MessageType.Html);

                //db.UserProfiles.Remove(usrp);
                //db.SaveChanges();
            }
        }

        [TestMethod]
        public void LoadExistingIncident()
        {
            using (var db = new StationCADDb())
            {
                Incident inc = db.Incidents
                    .Include("Organization")
                    .Include("LocationAddresses")
                    .Include("Notes")
                    .Include("Units")
                    .Where(x => x.LocalIncidentID == "F16001462").FirstOrDefault();
                if (inc != null)
                {
                    string json = JsonUtil<Incident>.ToJson(inc);
                    Console.WriteLine(json);
                }
                else
                {
                    Console.WriteLine("Couldn't find it.");
                }
            }
        }

        [TestMethod]
        public void ParseDate()
        {
            DateTime now = DateTime.Now;
            //string dt = "28-07-2016  14:09";
            //string dt = "14:09:00";
            string dt = "28/07/2016";
            string[] dtParts = dt.Split(' ');
            List<string> dtTrimmed = new List<string>();
            string[] dParts;
            string[] tParts;
            DateTime dtParsed = now;
            foreach (string item in dtParts)
            {
                if (item != string.Empty)
                    dtTrimmed.Add(item);
            }
            dtParts = dtTrimmed.ToArray();
            if (dtParts.Count() == 1)
            {
                // Are there date components?
                dParts = dtParts[0].Split('-');
                if (dParts.Count() > 1)
                { dtParsed = new DateTime(int.Parse(dParts[2]), int.Parse(dParts[1]), int.Parse(dParts[0]), 0, 0, 0); }
                dParts = dtParts[0].Split('/');
                if (dParts.Count() > 1)
                { dtParsed = new DateTime(int.Parse(dParts[2]), int.Parse(dParts[1]), int.Parse(dParts[0]), 0, 0, 0); }
                // Are there time components?
                tParts = dtParts[0].Split(':');
                if (tParts.Count() > 1)
                { dtParsed = new DateTime(now.Year, now.Month, now.Day, int.Parse(tParts[0], NumberStyles.Any), int.Parse(tParts[1], NumberStyles.Any), int.Parse(tParts[2], NumberStyles.Any)); }
            }
            else
            {
                dParts = dtParts[0].Split('-');
                tParts = dtParts[1].Split(':');
                dtParsed = new DateTime(int.Parse(dParts[2]), int.Parse(dParts[1]), int.Parse(dParts[0]), int.Parse(tParts[0]), int.Parse(tParts[1]), 0);
            }

            Console.WriteLine(dtParsed.ToString());
        }

        [TestMethod]
        public void TestDispatchHtmlParsing()
        {
            string data;
            using (StreamReader sr = new StreamReader(@"TestData\UnitClearReport22.htm"))
            { data = sr.ReadToEnd(); }
            
            if (data.Length > 0)
            {

                //var html = new HtmlDocument();
                //html.LoadHtml(data); // load a string 
                //var root = html.DocumentNode;
                //var nodes = root.Descendants("td");

                
                //int index = 0;
                //foreach(var item in nodes)
                //{
                //    Console.WriteLine(string.Format("[{0}] - {1}", index,item.InnerText));
                //    index++;
                //}

                DispatchManager dispMgr = new DispatchManager();
                ChesCoPAEventMessage eventInc = dispMgr.ParseEventHtml(data);

                string json = JsonUtil<ChesCoPAEventMessage>.ToJson(eventInc);
                Console.WriteLine(json);
            }
        }
    }
}
