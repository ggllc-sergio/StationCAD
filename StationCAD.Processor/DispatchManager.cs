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

namespace StationCAD.Processor
{
    public class DispatchManager <T>
        where T : DispatchEvent
    {

        public void ProcessEvent(Organization organization, string rawEvent)
        {

            // Parse the raw message
            ChesCoPAEventMessage dispEvent = ParseEventText(rawEvent);

            // Persist to the Database
            using (var db = new StationCADDb())
            {
                // Does this incident exist in the database with the CAD Event ID and Organization ID
                Incident incident = db.Incidents.Where(x => x.LocalIncidentID == dispEvent.Event && x.OrganizationId == organization.Id).FirstOrDefault();
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
                    ex.ToString();
                }
            }

            // Create Notifications

            // Task Parallel Library - Send notifications
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
                        if (line.IndexOf("Dispatch") != 0)
                            result.ReportType = ReportType.Dispatch;
                        if (line.IndexOf("Update") != 0)
                            result.ReportType = ReportType.Update;
                        if (line.IndexOf("Clear") != 0)
                            result.ReportType = ReportType.Clear;
                        if (line.IndexOf("Close") != 0)
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
                                        result.Units.Add(new UnitEntry { Unit = unitPieces[0].Trim(), Disposition = disposition, TimeStamp = unitPieces[2].Trim() });
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
                eventMessage.GeoLocations = new List<GeoLocation>();
                foreach(GoogleResult item in results.results)
                {
                    eventMessage.GeoLocations.Add(new GeoLocation
                    {
                        FormattedAddress = item.formatted_address,
                        Type = item.types[0],
                        PartialMatch = item.partial_match,
                        PlaceID = item.place_id,
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
            catch(Exception ex)
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
            string[] dtParts = eventMessage.CallTime.Split(' ');
            List<string> dtTrimmed = new List<string>();
            foreach (string item in dtParts)
            {
                if (item != string.Empty)
                    dtTrimmed.Add(item);
            }
            dtParts = dtTrimmed.ToArray();
            string[] dParts = dtParts[0].Split('-');
            string[] tParts = dtParts[1].Split(':');
            incident.DispatchedDateTime = new DateTime(int.Parse(dParts[2]), int.Parse(dParts[1]), int.Parse(dParts[0]), int.Parse(tParts[0]), int.Parse(tParts[1]), 0);

            // Incident Type Info 
            if (incident.Id == 0)
            {
                incident.IncidentTypeCode = eventMessage.EventTypeCode;
                incident.IncidentSubTypeCode = eventMessage.EventSubTypeCode;
                incident.FinalIncidentTypeCode = eventMessage.EventTypeCode;
                incident.FinalIncidentTypeCode = eventMessage.EventTypeCode;
            }
            else
            {
                incident.FinalIncidentTypeCode = eventMessage.EventTypeCode;
                incident.FinalIncidentTypeCode = eventMessage.EventTypeCode;
            }

            // Incident Location
            incident.LocationAddresses = new  List<IncidentAddress>();
            
            foreach(GeoLocation local in eventMessage.GeoLocations)
            {
                IncidentAddress addr = new IncidentAddress();
                addr.RawAddress = eventMessage.Address;
                addr.Municipality = eventMessage.LocationMunicipality != null ? eventMessage.LocationMunicipality.Name : eventMessage.Municipality;
                addr.FormattedAddress = local.FormattedAddress;
                addr.XCoordinate = local.Latitude;
                addr.YCoordinate = local.Longitude;
                addr.PlaceID = local.PlaceID;
                foreach(GoogleAddressComponent addrComp in local.AddressComponents)
                {
                    switch (addrComp.types[0])
                    {
                        case "street_number":

                            break;

                    }
                }
            }
            // Caller Info
            incident.CallerName = eventMessage.CallerName;
            incident.CallerAddress = eventMessage.CallerAddress;
            incident.CallerPhone = eventMessage.CallerPhoneNumber;


        }
    }


}
