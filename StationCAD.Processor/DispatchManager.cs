using System;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Linq.Expressions;

using StationCAD.Model;
using StationCAD.Model.DataContexts;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;
using System.Globalization;

namespace StationCAD.Processor
{
    public class DispatchManager <T>
        where T : DispatchEvent
    {

        public void ProcessEvent(Organization organization, string rawEvent)
        {
            // Parse the raw message
            ChesCoPAEventMessage dispEvent = ParseEventText(rawEvent);
            Incident incident;
            // Persist to the Database
            using (var db = new StationCADDb())
            {
                // Does this incident exist in the database with the CAD Event ID and Organization ID
                incident = db.Incidents
                    .Include("Organization")
                    .Include("LocationAddresses")
                    .Include("Notes")
                    .Include("Units")
                    .Where(x => x.LocalIncidentID == dispEvent.Event && x.OrganizationId == organization.Id)                    
                    .FirstOrDefault();
                if (incident == null)
                    incident = new Incident(organization);

                PopulateIncidentFromChesCoEvent(dispEvent, ref incident);
                if (incident.Id == 0)
                    db.Incidents.Add(incident);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                // Create Notifications
                // 1. Get list of users by org affiliation
                List<OrganizationUserNotifcation> notifications = NotificationManager.CreateNotifications(incident);
                db.OrganizationUserNotifcations.AddRange(notifications);
                db.SaveChanges();
                // Task Parallel Library - Send notifications
                NotificationManager.NotifyUsers(ref notifications);
                db.SaveChanges();

            }
        }

        public ChesCoPAEventMessage ParseEventText(string rawMessage)
        {
            ChesCoPAEventMessage result = new ChesCoPAEventMessage();

            if (rawMessage.Length > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ChesCoPAEventMessage));
                var lines = rawMessage.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                PropertyDescriptor prop = null;
                PropertyDescriptor currentProperty = null;
                PropertyDescriptor previousProperty = null;
                string propertyName = string.Empty;
                string eventAddress = string.Empty;

                foreach(string line in lines)
                {
                    if (result.Title == null)
                    {
                        result.Title = line;
                        if (line.IndexOf("Dispatch") != -1)
                            result.ReportType = ReportType.Dispatch;
                        if (line.IndexOf("Update") != -1)
                            result.ReportType = ReportType.Update;
                        if (line.IndexOf("Clear") != -1)
                            result.ReportType = ReportType.Clear;
                        if (line.IndexOf("Close") != -1)
                            result.ReportType = ReportType.Close;
                    }
                    else
                    {
                        var pieces = line.Split(new[] { ':' }, 2);
                        propertyName = (pieces.Count() > 1 ? pieces[0] : propertyName);
                        foreach (PropertyDescriptor property in properties)
                        {
                            // Look for an existing property
                            if (property.DisplayName == propertyName)
                            {
                                currentProperty = property;
                                prop = property;
                            }
                        }
                        if (prop == null && previousProperty != null)
                            prop = previousProperty;

                        if (prop != null)
                        {
                            switch (prop.DisplayName)
                            {
                                case "Address":
                                    if (pieces.Count() == 1)
                                    {
                                        eventAddress = string.Format("{0} {1}", eventAddress, line);
                                        prop.SetValue(result, eventAddress);
                                    }
                                    break;
                                case "Units":
                                    var unitPieces = line.Split(new[] { '\t' });
                                    if (unitPieces.Count() == 3)
                                    {
                                        string disposition = unitPieces[1].Length > 0 ? unitPieces[1].Trim() : result.ReportType.ToString();
                                        string timeStamp = unitPieces[2].Length > 0 ? unitPieces[2].Trim() : result.CallTime;
                                        result.Units.Add(new UnitEntry { Unit = unitPieces[0].Trim(), Disposition = disposition, TimeStamp = timeStamp });
                                    }
                                    
                                    break;
                                case "Event Comments":
                                    var commentPieces = line.Split(new[] { '-' }, 2);
                                    if (commentPieces.Count() == 2)
                                        result.Comments.Add(new EventComment { TimeStamp = commentPieces[0].Trim(), Comment = commentPieces[1].Trim() });
                                    break;
                                default:
                                    if (pieces.Count() > 1 && pieces[1].Length > 0)
                                    {
                                        prop.SetValue(result, pieces[1].Trim());
                                        previousProperty = currentProperty;
                                    }
                                    break;
                            }
                        }
                    }
                }
                // Parse Location info for mapping...
                ParseLocationforMapping(ref result);
            }

            return result;
        }
        

        protected void ParseLocationforMapping(ref ChesCoPAEventMessage eventMessage)
        {
            // Build Address 
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}", eventMessage.Address.Trim(), "+");
            if (eventMessage.LocationMunicipality != null)
            {
                sb.Append(eventMessage.LocationMunicipality.Name.Replace(" ", "+"));
            }
            GoogleGeoCodeResponse results = GEOCodeAddress(sb.ToString());
            // If results are ok, save the latitude/longitude
            if (results != null && results.status == "OK")
            {

                string googleAPIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
                if (googleAPIKey == null)
                    throw new ApplicationException("Unable to find Google API Key");
                eventMessage.GeoLocations = new List<GeoLocation>();
                foreach(GoogleResult item in results.results)
                {
                    string mapUrl = string.Format("https://maps.googleapis.com/maps/api/staticmap?zoom=16&size=400x400&markers=color:red%7C{0},{1}&key={2}", item.geometry.location.lat, item.geometry.location.lng, googleAPIKey);
                    eventMessage.GeoLocations.Add(new GeoLocation
                    {
                        FormattedAddress = item.formatted_address,
                        Type = item.types[0],
                        PartialMatch = item.partial_match,
                        PlaceID = item.place_id,
                        MapUrl = mapUrl,
                        Latitude = item.geometry.location.lat,
                        Longitude = item.geometry.location.lng,
                        AddressComponents = item.address_components
                    });
                }
            }

        }

        protected static GoogleGeoCodeResponse GEOCodeAddress(String Address)
        {
            try
            {
                string googleAPIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
                if (googleAPIKey == null)
                    throw new ApplicationException("Unable to find Google API Key");
                var address = String.Format("https://maps.google.com/maps/api/geocode/json?address={0}&key={1}", Address.Replace(" ", "+"), googleAPIKey);
                var result = new System.Net.WebClient().DownloadString(address);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                return jss.Deserialize<GoogleGeoCodeResponse>(result);
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
            }
            return null;
        }

        protected void PopulateIncidentFromChesCoEvent(ChesCoPAEventMessage eventMessage, ref Incident incident)
        {
            incident.Title = eventMessage.Title;
            incident.LocalIncidentID = eventMessage.Event;
            incident.LocalBoxArea = eventMessage.ESZ;
            // Times
            DateTime callTime = ParseChesCoEventDate(eventMessage.CallTime, DateTime.Now);
            incident.DispatchedDateTime = callTime != DateTime.MinValue ? callTime : DateTime.Now;

            // Incident Type Info 
            if (incident.Id == 0)
            {
                incident.IncidentTypeCode = eventMessage.EventTypeCode;
                incident.IncidentSubTypeCode = eventMessage.EventSubTypeCode;
                incident.FinalIncidentTypeCode = eventMessage.EventTypeCode;
                incident.FinalIncidentSubTypeCode = eventMessage.EventSubTypeCode;
            }
            else
            {
                incident.FinalIncidentTypeCode = eventMessage.EventTypeCode;
                incident.FinalIncidentSubTypeCode = eventMessage.EventSubTypeCode;
            }

            // Incident Location
            
            foreach(GeoLocation local in eventMessage.GeoLocations)
            {
                // Look for an existing Address
                IncidentAddress addr = incident.LocationAddresses.Where(x => x.FormattedAddress == local.FormattedAddress).FirstOrDefault();
                if (addr == null)
                    addr = new IncidentAddress();
                addr.RawAddress = eventMessage.Address;
                addr.Municipality = eventMessage.LocationMunicipality != null ? eventMessage.LocationMunicipality.Name : eventMessage.Municipality;
                addr.FormattedAddress = local.FormattedAddress;
                addr.XCoordinate = local.Latitude;
                addr.YCoordinate = local.Longitude;
                addr.PlaceID = local.PlaceID;
                addr.MapUrl = local.MapUrl;
                foreach(GoogleAddressComponent addrComp in local.AddressComponents)
                {
                    switch (addrComp.types[0])
                    {
                        case "street_number":
                            addr.Number = addrComp.long_name;
                            break;

                        case "route":
                            addr.Street = addrComp.long_name;
                            break;

                        case "administrative_area_level_3":
                            addr.Municipality = addrComp.long_name;
                            break;

                        case "administrative_area_level_2":
                            addr.County = addrComp.long_name;
                            break;

                        case "administrative_area_level_1":
                            addr.State = addrComp.long_name;
                            break;
                            
                        case "postal_code":
                            addr.PostalCode = addrComp.long_name;
                            break;

                    }
                }
                if (addr.Id == 0)
                    incident.LocationAddresses.Add(addr);
            }

            // Caller Info
            incident.CallerName = eventMessage.CallerName;
            incident.CallerAddress = eventMessage.CallerAddress;
            incident.CallerPhone = eventMessage.CallerPhoneNumber;

            if (eventMessage.Units != null && eventMessage.Units.Count > 0)
            {
                string localUnits = string.Empty;
                foreach (UnitEntry unit in eventMessage.Units)
                {
                    IncidentUnit item = incident.Units.Where(x => x.UnitID == unit.Unit && x.Disposition == unit.Disposition).FirstOrDefault();
                    if (item == null)
                        item = new IncidentUnit();
                    DateTime ts = ParseChesCoEventDate(unit.TimeStamp, incident.DispatchedDateTime);
                    item.UnitID = unit.Unit;
                    item.Disposition = unit.Disposition;
                    item.EnteredDateTime = ts != DateTime.MinValue ? ts : incident.DispatchedDateTime;
                    if (item.Id == 0)
                        incident.Units.Add(item);
                    if (unit.Unit.Contains(eventMessage.Beat))
                        localUnits += string.Format("{0} ", unit.Unit);
                }
                incident.LocalUnits = localUnits;
            }

            if (eventMessage.Comments != null && eventMessage.Comments.Count > 0)
            {
                foreach (EventComment note in eventMessage.Comments)
                {
                    DateTime ts = ParseChesCoEventDate(note.TimeStamp, incident.DispatchedDateTime);
                    IncidentNote item = incident.Notes.Where(x => x.Message == note.Comment && x.EnteredDateTime == ts).FirstOrDefault();
                    if (item == null)
                        item = new IncidentNote();
                    item.Message = note.Comment;
                    item.EnteredDateTime = ts != DateTime.MinValue ? ts : incident.DispatchedDateTime;
                    if (item.Id == 0)
                        incident.Notes.Add(item);
                }
            }
        }


        protected DateTime ParseChesCoEventDate(string eventDate, DateTime dispatchTime)
        {
            // Times
            try
            {
                if (eventDate.Length == 0)
                    return DateTime.MinValue;
                string[] dtParts = eventDate.Split(' ');
                List<string> dtTrimmed = new List<string>();
                string[] dParts;
                string[] tParts;
                DateTime dtParsed = dispatchTime;
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
                    { dtParsed = new DateTime(dispatchTime.Year, dispatchTime.Month, dispatchTime.Day, int.Parse(tParts[0], NumberStyles.Any), int.Parse(tParts[1], NumberStyles.Any), int.Parse(tParts[2], NumberStyles.Any)); }
                }
                else
                {
                    dParts = dtParts[0].Split('-');
                    tParts = dtParts[1].Split(':');
                    dtParsed = new DateTime(int.Parse(dParts[2]), int.Parse(dParts[1]), int.Parse(dParts[0]), int.Parse(tParts[0]), int.Parse(tParts[1]), 0);
                }
                return dtParsed;
            }
            catch
            { return DateTime.MinValue; }
        }


        protected int ParallelismFactor
        {
            get
            {
                int dop = 1;
                int procCnt = Environment.ProcessorCount;
                // if the available procs are 4 or more, use half.
                // otherwise just use 1.
                if (procCnt > 3)
                { dop = procCnt / 2; }
                else
                { dop = 1; }

                return dop;
            }
        }
    }


}
