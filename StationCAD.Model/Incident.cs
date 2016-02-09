using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using StationCAD.Model.Notifications.Clickatell;
using StationCAD.Model.Notifications.OneSignal;
using StationCAD.Model.Notifications.Mailgun;

namespace StationCAD.Model
{
    public class Incident : BaseModel
    {
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        public int CADIdentifier { get; set; }

        public Guid IncidentIdentifier { get; set; }

        public DateTime EnteredDateTime { get; set; }

        public DateTime DispatchedDateTime { get; set; }

        public string ConsoleID { get; set; }


        #region Local Incident Info 

        public string IncidentTypeCode { get; set; }
        public string IncidentType { get; set; }

        public string FinalIncidentTypeCode { get; set; }
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

        public string RAWCADIncidentData { get; set; }

        public SMSNotification GetSMSNotification(User user)
        {
            SMSNotification sms = new SMSNotification();
            sms.Recipients = new List<string>();
            sms.Recipients.Add(user.NotificationCellPhone);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("D: {0}", this.DispatchedDateTime));
            sb.AppendLine(string.Format("{0}", this.IncidentType));
            sb.AppendLine(string.Format("{0}", this.LocationAddress.NotificationAddress));
            var firstNote = this.Notes.OrderBy(x => x.EnteredDateTime).FirstOrDefault();
            sb.AppendLine(string.Format("NOTES: {0}", firstNote.Message));
            switch (this.Organization.Type)
            {
                case OrganizationType.Fire:
                    sb.AppendLine(string.Format("BOX: {0}", this.LocalFireBox));
                    break;
                case OrganizationType.EMS:
                    sb.AppendLine(string.Format("BOX: {0}", this.LocalEMSBox));
                    break;
                case OrganizationType.Police:
                    sb.AppendLine(string.Format("BOX: {0}", this.LocalPoliceBox));
                    break;
                case OrganizationType.GovernmentOEM:
                default:
                    sb.AppendLine(string.Format("BOX: F{0}, E{1}, P{2}", this.LocalFireBox, this.LocalEMSBox, this.LocalPoliceBox));
                    break;
            }
            sms.Text = sb.ToString();
            return sms;
        }

        public SMSEmailNotification GetSMSEmailNotification(User user)
        {
            SMSEmailNotification smsEmail = new SMSEmailNotification();
            smsEmail.MessageBody = GetSMSNotification(user).Text;
            return smsEmail;
        }

        public EmailNotification GetEmailNotification()
        {
            EmailNotification email = new EmailNotification();
            email.MessageSubject = string.Format("{0} - Incident: {2}", this.Organization.Name, this.IncidentType);
            email.MessageBody = this.RAWCADIncidentData;
            email.OrganizationName = this.Organization.Name;
            return email;
        }

        public PushNotificationCreate GetPushNotification()
        {
            PushNotificationCreate push = new PushNotificationCreate();
            StringBuilder sb = new StringBuilder();
            push.Headings = new Dictionary<string, string>();
            push.Headings.Add("en", string.Format("{0} - {1}", this.Organization.Name, this.IncidentType));
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
        public LocationType IncidentLocationType { get; set; }

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

    public enum LocationType
    {
        AddressResidential=1,
        AddressCommercial,
        Intersection
    }
    
}
