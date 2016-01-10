using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCAD.Model
{
    public class Incident
    {
        public string CADIdentifier { get; set; }

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

        public Address LocationAddress { get; set; }

        #endregion

        #region Local Incident Caller 
        public string CallerName { get; set; }

        public string CallerAddress { get; set; }

        public string CallerPhone { get; set; }

        #endregion

        public List<Note> Notes { get; set; }
        public List<Event> Events { get; set; }
    }

    public class Address
    {
        public string LocationType { get; set; }

        public string Building { get; set; }

        public string Development { get; set; }
        
        public string OccupantName { get; set; }
        
        public string Number { get; set; }

        public string Street { get; set; }

        public string XStreet { get; }

        public string Municipality { get; set; }

        public string XCoordinate { get; set; }

        public string YCoordinate { get; set; }
    }

    public class Note
    {
        public DateTime EnteredDateTime { get; set; }

        public string Author { get; set; }

        public string Message { get; set; }
    }

    public class Event
    {
        public DateTime EnteredDateTime { get; set; }

        public string UnitID { get; set; }

        public string Disposition { get; set; }

        public Note EventNote { get; set; }
    }
}
