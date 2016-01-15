using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCAD.Model
{
    public class User : BaseModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string IdentificationNumber { get; set; }

        public string NoificationEmail { get; set; }

        public string NotificationCellPhone { get; set; }

        public string NotifcationPushMobile { get; set; }

        public string NotifcationPushBrowser { get; set; }

    }

    public class OrganizationUser : BaseModel
    {
        public int OrganizationId { get; set; }

        public int UserId { get; set; }

        public OrganizationUserStatus Status { get; set; }

        public OrganizationUserRole Role { get; set; }
        
    }

    public class OrganizationUserNotifcation : BaseModel
    {
        public int OrganizationUserId { get; set; }

        public OrganizationUserNotifcationType NotifcationType { get; set; }

        public string Message { get; set; }

        public DateTime Sent { get; set; }
    }


    public enum OrganizationUserNotifcationType
    {
        Email = 1,
        TextMessage,
        PushMobile,
        PushBrowser
    }

    public enum OrganizationUserStatus
    {
        Active = 1,
        Inactive,
        Suspended
    }

    public enum OrganizationUserRole
    {
        User = 1,
        Administrator,
        Owner
    }

}
