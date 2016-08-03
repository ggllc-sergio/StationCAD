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
                var usr = new User();
                usr.FirstName = string.Format("FirstName_{0}", DateTime.Now.Ticks);
                usr.LastName = string.Format("LastName_{0}", DateTime.Now.Ticks);
                usr.IdentificationNumber = DateTime.Now.Ticks.ToString();
                usr.UserName = string.Format("{0}.{1}", usr.FirstName, usr.LastName);
                usr.OrganizationAffiliations = new List<UserOrganizationAffiliation>();
                usr.OrganizationAffiliations.Add(new UserOrganizationAffiliation { Status = OrganizationUserStatus.Active, Role = OrganizationUserRole.User, OrganizationId = 1 });
                usr.NotificationEmail = "skip513@gmail.com";
                usr.MobileDevices = new List<UserMobileDevice>();
                usr.MobileDevices.Add(new UserMobileDevice { Carrier = MobileCarrier.ATT, EnableSMS = true, MobileNumber = "6108833253" });

                db.Users.Add(usr);
                db.SaveChanges();

                Assert.IsTrue(usr.Id > 0);

                List<User> users = db.Users
                    .Include("MobileDevices")
                    .Where(w => w.OrganizationAffiliations.Where(x => x.OrganizationId == 1).Count() > 0)
                    .ToList<User>();

                var afterUser = db.Users
                    .Include("OrganizationAffiliations")
                    .Include("MobileDevices")
                    .Where(x => x.IdentificationNumber == usr.IdentificationNumber)
                    .FirstOrDefault();
                db.Users.Remove(afterUser);
                db.SaveChanges();
            }
        }
    }
}
