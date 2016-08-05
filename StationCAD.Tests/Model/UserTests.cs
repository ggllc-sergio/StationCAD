using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using StationCAD.Model;
using StationCAD.Model.DataContexts;
using System.Collections.Generic;

namespace StationCAD.Tests.Model
{
    [TestClass]
    public class UserTests
    {
        protected StationCADDb _staCadDB;
        protected StationCADDb staCadDB
        {
            get
            {
                if (_staCadDB == null)
                    _staCadDB = new StationCADDb();
                return _staCadDB;
            }
        }

        [TestMethod]
        public void AddUser()
        {
            using (var db = new StationCADDb())
            {
                var usr = new UserProfile();
                usr.FirstName = string.Format("FirstName_{0}", DateTime.Now.Ticks);
                usr.LastName = string.Format("LastName_{0}", DateTime.Now.Ticks);
                usr.IdentificationNumber = DateTime.Now.Ticks.ToString();
                //usr.UserName = string.Format("{0}.{1}", usr.FirstName, usr.LastName);
                usr.OrganizationAffiliations = new List<UserOrganizationAffiliation>();
                usr.OrganizationAffiliations.Add(new UserOrganizationAffiliation { Status = OrganizationUserStatus.Active, Role = OrganizationUserRole.User });
                usr.NotificationEmail = "skip513@gmail.com";
                usr.MobileDevices = new List<UserMobileDevice>();
                usr.MobileDevices.Add(new UserMobileDevice { Carrier = MobileCarrier.ATT, EnableSMS = true, MobileNumber = "6108833253" });

                db.UserProfiles.Add(usr);
                db.SaveChanges();

                Assert.IsTrue(usr.Id > 0);

                List<UserProfile> users = db.UserProfiles
                    .Include("MobileDevices")
                    .Include("OrganizationAffiliations")
                    .Where(w => w.OrganizationAffiliations.Where(x => x.CurrentOrganization.Id == 1).Count() > 0)
                    .ToList<UserProfile>();

                var afterUser = db.UserProfiles
                    .Include("OrganizationAffiliations")
                    .Include("MobileDevices")
                    .Where(x => x.IdentificationNumber == usr.IdentificationNumber)
                    .FirstOrDefault();
                db.UserProfiles.Remove(afterUser);
                db.SaveChanges();
            }
        }
    }
}
