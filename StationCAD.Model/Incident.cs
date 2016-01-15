using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StationCAD.Data.Mongo;

namespace StationCAD.Model
{
    public class Incident : BaseModel
    {
        public int OrganizationId { get; set; }

        public int CADIdentifier { get; set; }

        public Guid IncidentIdentifier { get; set; }

        public DateTime EnteredDateTime { get; set; }

        public DateTime DispatchedDateTime { get; set; }

        public string ConsoleID { get; set; }


        #region Local Incident Info 

        public string IncidentType { get; set; }

        public string FinalIncidentType { get; set; }

        public string IncidentPriority { get; set; }

        public string FinalIncidentPriority { get; set; }

        public string LocalIncidentID { get; set; }

        public string LocalXRefID { get; set; }

        public string LocalFireBox { get; set; }

        public string LocalEMSBox { get; set; }

        public string LocalPoliceBox { get; set; }

        public string LocationGroup { get; set; }

        public string LocationSection { get; set; }

        public IncidentAddress LocationAddress { get; set; }

        #endregion

        #region Local Incident Caller 

        public string CallerName { get; set; }

        public string CallerAddress { get; set; }

        public string CallerPhone { get; set; }

        #endregion

        public ICollection<IncidentNote> Notes { get; set; }
        public ICollection<IncidentEvent> Events { get; set; }
    }

    public class IncidentAddress : BaseModel
    {
        public string LocationType { get; set; }

        public string Building { get; set; }

        public string Development { get; set; }
        
        public string OccupantName { get; set; }
        
        public string Number { get; set; }

        public string Street { get; set; }

        public string XStreet1 { get; set; }

        public string XStreet2 { get; set; }

        public string County { get; set; }

        public string Municipality { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }
        
        public string XCoordinate { get; set; }

        public string YCoordinate { get; set; }

        public string Gelocation { get; set; }
    }

    public class IncidentNote : BaseModel
    {

        public DateTime EnteredDateTime { get; set; }

        public string Author { get; set; }

        public string Message { get; set; }
    }

    public class IncidentEvent : BaseModel
    {
        public DateTime EnteredDateTime { get; set; }

        public string UnitID { get; set; }

        public string Disposition { get; set; }

        public IncidentNote EventNote { get; set; }
    }
}
