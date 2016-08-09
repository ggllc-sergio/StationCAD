using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using StationCAD.Model.Notifications.Clickatell;
using StationCAD.Model.Notifications.OneSignal;
using StationCAD.Model.Notifications.Mailgun;
using Newtonsoft.Json;

namespace StationCAD.Model
{
    public class Incident : BaseModel
    {

        public Incident() { }

        public Incident(Organization org)
        {
            this.IncidentIdentifier = Guid.NewGuid();
            this.LocationAddresses = new List<IncidentAddress>();
            this.Units = new List<IncidentUnit>();
            this.Notes = new List<IncidentNote>();
            this.OrganizationId = org.Id;
        }

        /// <summary>
        /// The Organization (First Due Station) for the Incident
        /// </summary>
        public int OrganizationId { get; set; }

        public virtual Organization Organization
        { get; set; }

        /// <summary>
        /// The Identifier specific to the CAD system that delivered it.
        /// </summary>
        public int CADIdentifier { get; set; }

        public string Title { get; set; }

        public ReportType EventType { get; set; }

        public Guid IncidentIdentifier { get; set; }

        [DisplayName("Call Time")]
        public DateTime DispatchedDateTime { get; set; }

        public string ConsoleID { get; set; }


        #region Local Incident Info 

        public string IncidentTypeCode { get; set; }

        public string IncidentSubTypeCode { get; set; }

        [DisplayName("Event Type Code")]
        public string FinalIncidentTypeCode { get; set; }

        [DisplayName("Event SubType Code")]
        public string FinalIncidentSubTypeCode { get; set; }
        
        [DisplayName("Event")]
        public string LocalIncidentID { get; set; }

        [DisplayName("Event ID")]
        public string EventID { get; set; }

        public string LocalXRefID { get; set; }

        public string LocalBoxArea { get; set; }
        
        public IncidentAddress PrimaryAddress
        {
            get
            {
                if (LocationAddresses != null && LocationAddresses.Count == 1)
                { return LocationAddresses.First(); }
                else
                { return null; }
            }
        }

        public virtual ICollection<IncidentAddress> LocationAddresses { get; set; }

        #endregion

        #region Local Incident Caller 

        public string CallerName { get; set; }

        public string CallerAddress { get; set; }

        public string CallerPhone { get; set; }

        #endregion

        [DisplayName("Event Comments")]
        public virtual ICollection<IncidentNote> Notes { get; set; }
        
        public string LocalUnits { get; set; }

        public ICollection<IncidentUnit> Units { get; set; }

        public string RAWCADIncidentData { get; set; }

        public SMSNotification GetSMSNotification(UserProfile user)
        {
            SMSNotification sms = new SMSNotification();
            sms.Recipients = new List<string>();
            sms.Recipients.Add(user.NotificationCellPhone);
            sms.Text = GetShortNotificationBody();
            return sms;
        }

        protected string GetShortNotificationBody()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("EVENT: {0}", this.LocalIncidentID));
            sb.AppendLine(string.Format("DISPATCH TIME: {0}", this.DispatchedDateTime));
            sb.AppendLine(string.Format("INCIDENT: {0} - {1}", this.IncidentTypeCode, this.IncidentSubTypeCode.Length > 0 ? this.IncidentSubTypeCode : "N/A"));
            sb.AppendLine("LOCATION:");
            sb.AppendLine(string.Format("{0}", this.PrimaryAddress.NotificationAddress));
            var firstNote = this.Notes.OrderBy(x => x.EnteredDateTime).FirstOrDefault();
            sb.AppendLine(string.Format("NOTES: {0}", firstNote.Message));
            sb.AppendLine(string.Format("BOX: {0}", this.LocalBoxArea));
            sb.AppendLine(string.Format("UNITS: {0}", this.LocalUnits));
            return sb.ToString();
        }

        public SMSEmailNotification GetSMSEmailNotification(OrganizationUserAffiliation userOrgAffiliation)
        {
            SMSEmailNotification smsEmail = new SMSEmailNotification();
            smsEmail.MobileNumber = userOrgAffiliation.CurrentUserProfile.MobileDevices.First().MobileNumber;
            smsEmail.Carrier = userOrgAffiliation.CurrentUserProfile.MobileDevices.First().Carrier;
            smsEmail.MessageBody = GetShortNotificationBody();
            smsEmail.OrganizationName = userOrgAffiliation.CurrentOrganization.Name;
            return smsEmail;
        }

        public EmailNotification GetEmailNotification(OrganizationUserAffiliation userOrgAffiliation)
        {
            EmailNotification email = new EmailNotification();
            email.Recipient = userOrgAffiliation.CurrentUserProfile.NotificationEmail;
            email.MessageSubject = string.Format("{0} - Incident: {1}", userOrgAffiliation.CurrentOrganization.Name, this.IncidentTypeCode);
            email.MessageBody = GetShortNotificationBody();
            email.OrganizationName = userOrgAffiliation.CurrentOrganization.Name;
            return email;
        }

        public PushNotificationCreate GetPushNotification()
        {
            PushNotificationCreate push = new PushNotificationCreate();
            StringBuilder sb = new StringBuilder();
            push.Headings = new Dictionary<string, string>();
            push.Headings.Add("en", string.Format("{0} - {1}", this.Organization.Name, this.IncidentTypeCode));
            push.Url = "http://station-cad.graphitegear.com";
            push.IncludeTags.Add(new PushNotificationTag { Key = "OrgTag", Value = this.Organization.Tag, Relation = "=" });
            push.Data.Add(new KeyValuePair<string, string>("IncidentID", this.Id.ToString()));
            push.Contents = new Dictionary<string, string>();
            push.Contents.Add("en", sb.ToString());
            return push;
        }

        public List<IncidentNotification> GetNotifications(OrganizationUserAffiliation userOrgAffiliation)
        {
            List<IncidentNotification> results = new List<IncidentNotification>();

            if (userOrgAffiliation.CurrentUserProfile.NotifcationPushMobile != null)
                results.Add(GetPushNotification());

            return results;
        }
    }
    public class IncidentAddress : Address
    {
        [JsonIgnore]
        public virtual Incident Incident { get; set; }

        public LocationType IncidentLocationType { get; set; }

        public string RawAddress { get; set; }
        public string FormattedAddress { get; set; }

        public bool GeoPartialMatch { get; set; }

        public string NotificationAddress
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                switch(this.IncidentLocationType)
                {
                    case LocationType.Intersection:
                        sb.AppendFormat("{1}{0}", Environment.NewLine, this.Street);
                        break;
                    case LocationType.AddressResidential:
                    case LocationType.AddressCommercial:
                    default:
                        sb.AppendFormat("{1} {2}{0}", Environment.NewLine, this.Number, this.Street);
                        if (this.Building != null)
                            sb.AppendFormat("{1}{0}", Environment.NewLine, this.Building);
                        if (this.Development != null)
                            sb.AppendFormat("{1}{0}", Environment.NewLine, this.Development);
                        break;
                }
                if (this.XStreet1 != null)
                    sb.AppendFormat("X1: {1}{0}", Environment.NewLine, this.XStreet1);
                if (this.XStreet2 != null)
                    sb.AppendFormat("X2: {1}{0}", Environment.NewLine, this.XStreet2);
                sb.AppendFormat("{1} ({2}, {3}){0}", Environment.NewLine, this.Municipality, this.County, this.State);
                if (this.MapUrl != null)
                    sb.AppendFormat("Map: {1}{0}", Environment.NewLine, this.MapUrl);
                return sb.ToString();
            }
        }
    }

    public class IncidentNote : BaseModel
    {
        [JsonIgnore]
        public virtual Incident Incident { get; set; }
        public DateTime EnteredDateTime { get; set; }

        public string Author { get; set; }

        public string Message { get; set; }
    }

    public class IncidentUnit : BaseModel
    {
        [JsonIgnore]
        public virtual Incident Incident { get; set; }
        public DateTime EnteredDateTime { get; set; }

        public string UnitID { get; set; }

        public string Disposition { get; set; }

        public string Comment { get; set; }
    }

    public enum LocationType
    {
        AddressResidential=1,
        AddressCommercial,
        Intersection
    }
    
}
