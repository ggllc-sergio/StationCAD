using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StationCAD.Model
{
    public class ApplicationUserStore :
    UserStore<User, Role, string, UserLogin, UserRole, UserClaim>,
    IUserStore<User>,
    IDisposable
    {
        public ApplicationUserStore(DataContexts.IdentityDb context) : base(context) { }
    }

    public class User : IdentityUser<string, UserLogin, UserRole, UserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual UserProfile Profile { get; set; }
    }

    public class UserRole : IdentityUserRole { }

    public class Role : IdentityRole<string, UserRole> { }

    public class UserClaim : IdentityUserClaim { }

    public class UserLogin : IdentityUserLogin { }

    
    public class UserProfile : BaseModel
    {
        public virtual User SecurityUser { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [EmailAddress]
        public string AccountEmail { get; set; }

        public string IdentificationNumber { get; set; }

        [EmailAddress]
        public string NotificationEmail { get; set; }

        [Phone]
        public string NotificationCellPhone { get; set; }

        public string NotifcationPushMobile { get; set; }

        public string NotifcationPushBrowser { get; set; }

        public ICollection<string> MobileDeviceIds { get; set; }

        public virtual ICollection<UserAddress> Addresses { get; set; }

        public virtual ICollection<UserOrganizationAffiliation> OrganizationAffiliations { get; set; }
        
        public virtual ICollection<UserMobileDevice> MobileDevices { get; set; }

    }
    

    public class UserAddress : Address
    {
        public virtual UserProfile User { get; set; }
    }

    public class UserOrganizationAffiliation : BaseModel
    {
        public virtual UserProfile CurrentUser { get; set; }
        
        public virtual Organization CurrentOrganization { get; set; }

        public OrganizationUserStatus Status { get; set; }

        public OrganizationUserRole Role { get; set; }

        public virtual ICollection<OrganizationUserNotifcation> NotificationHistory { get; set; }

    }

    public class OrganizationUserNotifcation : BaseModel
    {
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

        public virtual UserProfile User { get; set; }

        public string MobileNumber { get; set; }
        public MobileCarrier Carrier { get; set; }
        
        public bool EnablePush { get; set; }
        public bool EnableSMS { get; set; }
        public virtual ICollection<UserMobileDeviceOrganization> UserMobileDeviceOrganizations { get; set; }

    }

    public class UserMobileDeviceOrganization : BaseModel
    {
        public virtual UserMobileDevice UserDevice { get; set; }
        
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
