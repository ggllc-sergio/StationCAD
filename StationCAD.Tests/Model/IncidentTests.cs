using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Linq;
using StationCAD.Model;
using StationCAD.Model.DataContexts;

namespace StationCAD.Tests.Model
{
    [TestClass]
    public class IncidentTests
    {
        [TestMethod]
        public void CreateIncident()
        {
            Incident inc = new Incident();
            inc.OrganizationId = 1;
            inc.CADIdentifier = 1;
            inc.CallerAddress = "Caller Address 1";
            inc.CallerName = "Caller Name 1";
            inc.CallerPhone = "Caller Phone 1";
            inc.ConsoleID = "Console 1";
            inc.DispatchedDateTime = DateTime.Now;
            inc.IncidentIdentifier = Guid.NewGuid();
            inc.Title = "Title";

            IncidentAddress add = new IncidentAddress();
            add.Number = "1";
            add.Street = "street";
            add.City = "city";
            inc.LocationAddresses = new List<IncidentAddress>();
            inc.LocationAddresses.Add(add);

            IncidentUnit unit = new IncidentUnit();
            unit.UnitID = "Unit 1";
            unit.EnteredDateTime = DateTime.Now;
            unit.Disposition = "DP";
            inc.Units = new List<IncidentUnit>();
            inc.Units.Add(unit);

            IncidentNote cmt = new IncidentNote();
            cmt.Message = "Comment 1";
            cmt.EnteredDateTime = DateTime.Now;
            cmt.Author = "Author";
            inc.Notes = new List<IncidentNote>();
            inc.Notes.Add(cmt);



            using (var db = new StationCADDb())
            {
                Incident existing = db.Incidents.Where(x => x.Title == "Title").FirstOrDefault();
                if (existing != null)
                {
                    db.Incidents.Remove(existing);
                    db.SaveChanges();
                }
                db.Incidents.Add(inc);
                db.SaveChanges();


                Incident saved = db.Incidents.Where(x => x.Title == "Title").FirstOrDefault();
                Assert.IsNotNull(saved);

                db.Incidents.Remove(saved);
                db.SaveChanges();
            }
        }
    }
}
