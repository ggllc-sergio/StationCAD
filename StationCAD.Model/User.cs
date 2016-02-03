using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace StationCAD.Model
{

    public class User : BaseModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string IdentificationNumber { get; set; }

        public string NotificationEmail { get; set; }

        public string NotificationCellPhone { get; set; }

        public string NotifcationPushMobile { get; set; }

        public string NotifcationPushBrowser { get; set; }

        public ICollection<string> MobileDeviceIds { get; set; }

        public virtual ICollection<UserOrganizationAffiliation> OrganizationAffiliations { get; set; }

        public virtual ICollection<OrganizationUserNotifcation> NotificationHistory { get; set; }

    }

    public class UserAddress : Address
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }

    public class UserOrganizationAffiliation : BaseModel
    {
        public int UserId { get; set; }
        public virtual User CurrentUser { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organization CurrentOrganization { get; set; }

        public OrganizationUserStatus Status { get; set; }

        public OrganizationUserRole Role { get; set; }
        
    }

    public class OrganizationUserNotifcation : BaseModel
    {

        public int UserOrganizationAffiliationId { get; set; }
        public virtual UserOrganizationAffiliation Affilitation { get; set; }

        public OrganizationUserNotifcationType NotifcationType { get; set; }

        public string MessageTitle { get; set; }

        public string MessageBody { get; set; }

        public DateTime Sent { get; set; }
    }


    public enum OrganizationUserNotifcationType
    {

        Email = 1,
        [Display(Name = "Text Message")]
        TextMessage,
        [Display(Name = "Push - Mobile Device")]
        PushMobile,
        [Display(Name = "Push - Browser")]
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
