using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using StationCAD.Model;
using StationCAD.Model.DataContexts;

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

                db.Users.Add(usr);
                db.SaveChanges();

                Assert.IsTrue(usr.Id > 0);

                var afterUser = db.Users.Where(x => x.IdentificationNumber == usr.IdentificationNumber).FirstOrDefault();
                db.Users.Remove(afterUser);
                db.SaveChanges();
            }
        }
    }
}
