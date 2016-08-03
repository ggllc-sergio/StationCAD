using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StationCAD.Model
{

    public class User : BaseModel
    {
        [Required(AllowEmptyStrings =false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        
        public string IdentificationNumber { get; set; }

        [EmailAddress]
        public string NotificationEmail { get; set; }

        public string NotificationCellPhone { get; set; }

        public string NotifcationPushMobile { get; set; }

        public string NotifcationPushBrowser { get; set; }

        public ICollection<string> MobileDeviceIds { get; set; }

        public virtual ICollection<UserOrganizationAffiliation> OrganizationAffiliations { get; set; }

        public virtual ICollection<OrganizationUserNotifcation> NotificationHistory { get; set; }

        public virtual ICollection<UserMobileDevice> MobileDevices { get; set; }

    }
    

    public class UserAddress : Address
    {
        [Required]
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }

    public class UserOrganizationAffiliation : BaseModel
    {
        [Required]
        public int UserId { get; set; }
        public virtual User CurrentUser { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        public virtual Organization CurrentOrganization { get; set; }

        public OrganizationUserStatus Status { get; set; }

        public OrganizationUserRole Role { get; set; }
        
    }

    public class OrganizationUserNotifcation : BaseModel
    {
        [Required]
        public int UserOrganizationAffiliationId { get; set; }
        public virtual UserOrganizationAffiliation Affilitation { get; set; }

        public OrganizationUserNotifcationType NotifcationType { get; set; }

        public string MessageTitle { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string MessageBody { get; set; }

        [Required]
        public DateTime Sent { get; set; }
        
        [NotMapped]
        public IncidentNotification Notification { get; set; }
    }

    public class UserMobileDevice : BaseModel
    {

        public virtual User User { get; set; }

        public string MobileNumber { get; set; }
        public MobileCarrier Carrier { get; set; }
        
        public bool EnablePush { get; set; }
        public bool EnableSMS { get; set; }
        public virtual ICollection<UserMobileDeviceOrganization> UserMobileDeviceOrganizations { get; set; }

    }

    public class UserMobileDeviceOrganization : BaseModel
    {
        public int UserMobileDeviceID { get; set; }
        public virtual UserMobileDevice UserDevice { get; set; }

        public int OrganizationID { get; set; }
        public virtual Organization Organization { get; set; }

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
