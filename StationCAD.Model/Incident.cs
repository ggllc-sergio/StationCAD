using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using StationCAD.Model.Notifications.Clickatell;
using StationCAD.Model.Notifications.OneSignal;
using StationCAD.Model.Notifications.Mailgun;

namespace StationCAD.Model
{
    public class Incident : BaseModel
    {

        public Incident() { }

        public Incident(Organization org)
        {
            this.IncidentIdentifier = Guid.NewGuid();
            this.OrganizationId = org.Id;
        }

        /// <summary>
        /// The Organization (First Due Station) for the Incident
        /// </summary>
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        /// <summary>
        /// The Identifier specific to the CAD system that delivered it.
        /// </summary>
        public int CADIdentifier { get; set; }
        
        public string Title { get; set; }

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

        public string LocalXRefID { get; set; }

        public string LocalBoxArea { get; set; }
        
        public IncidentAddress LocationAddress { get; set; }

        #endregion

        #region Local Incident Caller 

        public string CallerName { get; set; }

        public string CallerAddress { get; set; }

        public string CallerPhone { get; set; }

        #endregion

        [DisplayName("Event Comments")]
        public ICollection<IncidentNote> Notes { get; set; }
        
        public string LocalUnits { get; set; }

        public ICollection<IncidentEvent> Units { get; set; }

        public string RAWCADIncidentData { get; set; }

        public SMSNotification GetSMSNotification(User user)
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
            sb.AppendLine(string.Format("D: {0}", this.DispatchedDateTime));
            sb.AppendLine(string.Format("{0}", this.IncidentTypeCode));
            sb.AppendLine(string.Format("{0}", this.LocationAddress.NotificationAddress));
            var firstNote = this.Notes.OrderBy(x => x.EnteredDateTime).FirstOrDefault();
            sb.AppendLine(string.Format("NOTES: {0}", firstNote.Message));
            sb.AppendLine(string.Format("BOX: {0}", this.LocalBoxArea));
            sb.AppendLine(string.Format("Units: {0}", this.LocalUnits));
            return sb.ToString();
        }

        public SMSEmailNotification GetSMSEmailNotification(User user)
        {
            SMSEmailNotification smsEmail = new SMSEmailNotification();
            smsEmail.MessageBody = GetShortNotificationBody();
            return smsEmail;
        }

        public EmailNotification GetEmailNotification(User user)
        {
            EmailNotification email = new EmailNotification();
            email.MessageSubject = string.Format("{0} - Incident: {2}", this.Organization.Name, this.IncidentTypeCode);
            email.MessageBody = GetShortNotificationBody();
            email.OrganizationName = this.Organization.Name;
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

        public List<IncidentNotification> GetNotifications(User user)
        {
            List<IncidentNotification> results = new List<IncidentNotification>();

            if (user.NotifcationPushMobile != null)
                results.Add(GetPushNotification());

            return results;
        }
    }
    public class IncidentAddress : Address
    {
        []
        public int IncidentId { get; set; }
        public virtual Incident Incident { get; set; }

        public LocationType IncidentLocationType { get; set; }

        public string RawAddress { get; set; }
        public string FormattedAddress { get; set; }

        public string NotificationAddress
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                switch(this.IncidentLocationType)
                {
                    case LocationType.Intersection:
                        sb.AppendFormat("{0}", this.Street);
                        break;
                    case LocationType.AddressResidential:
                    case LocationType.AddressCommercial:
                    default:
                        sb.AppendFormat("{0} {1}", this.Number, this.Street);
                        sb.AppendFormat("{0}", this.Building);
                        sb.AppendFormat("{0}", this.Development);
                        break;
                }
                sb.AppendFormat("X1: {0}", this.XStreet1);
                sb.AppendFormat("X2: {0}", this.XStreet2);
                sb.AppendFormat("{0} ({1}, {2})", this.Municipality, this.County, this.State);
                return sb.ToString();
            }
        }
    }

    public class IncidentNote : BaseModel
    {
        public int IncidentId { get; set; }
        public virtual Incident Incident { get; set; }
        public DateTime EnteredDateTime { get; set; }

        public string Author { get; set; }

        public string Message { get; set; }
    }

    public class IncidentEvent : BaseModel
    {
        public int IncidentId { get; set; }
        public virtual Incident Incident { get; set; }
        public DateTime EnteredDateTime { get; set; }

        public string UnitID { get; set; }

        public string Disposition { get; set; }

        public IncidentNote EventNote { get; set; }
    }

    public enum LocationType
    {
        AddressResidential=1,
        AddressCommercial,
        Intersection
    }
    
}
