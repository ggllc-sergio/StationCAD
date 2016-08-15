using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text;
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

    public class OrgUserRegistration
    {
        public Organization Organization { get; set; }

        public List<UserRegistration> Users { get; set; }
    }

    public class UserRegistration
    {
        [EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Security Question")]
        public string SecurityQuestion { get; set; }

        [Display(Name = "Security Answer")]
        public string SecurityAnswer { get; set; }

        [EmailAddress]
        [Display(Name = "Account Email")]
        public string AccountEmail { get; set; }

        [Display(Name = "ID Number")]
        public string IdentificationNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Notification Email")]
        public string NotificationEmail { get; set; }

        [Phone]
        [Display(Name = "Notification Phone")]
        public string NotificationCellPhone { get; set; }

        [Display(Name = "Notification Push")]
        public string NotifcationPushMobile { get; set; }

        [Display(Name = "Notification Browser")]
        public string NotifcationPushBrowser { get; set; }

        public ICollection<string> MobileDeviceIds { get; set; }

        public virtual ICollection<UserAddress> Addresses { get; set; }

        public virtual ICollection<OrganizationUserAffiliation> OrganizationAffiliations { get; set; }
        
        public virtual ICollection<UserMobileDevice> MobileDevices { get; set; }

    }
    

    public class UserAddress : Address
    {
        public virtual UserProfile User { get; set; }

        public string  DisplayFormat
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                // 123 Main St
                sb.AppendFormat("{1}{2}{0}", Environment.NewLine,
                                    this.Number.Length > 0 ? this.Number : string.Empty,
                                    this.Street.Length > 0 ? this.Street : string.Empty);
                // Apt 101C
                if (this.Apartment.Length > 0)
                    sb.AppendFormat("Apt. {1}{0}", Environment.NewLine, this.Apartment);
                // Building 201
                if (this.Building.Length > 0)
                    sb.AppendFormat("Bldg. {1}{0}", Environment.NewLine, this.Building);
                // Hideaway Farms Development
                if (this.Development.Length > 0)
                    sb.AppendFormat("{1}{0}", Environment.NewLine, this.Development);
                // Downingtown, PA 19335 (East Brandywine Twp)
                sb.AppendFormat("{1}{2}{3}{4}{0}", Environment.NewLine,
                                    this.City.Length > 0 ? this.City + ", " : string.Empty,
                                    this.State.Length > 0 ? this.State + " " : string.Empty,
                                    this.PostalCode.Length > 0 ? this.PostalCode + " " : string.Empty,
                                    this.Municipality.Length > 0 ? "(" + this.Municipality +")" : string.Empty);
                return sb.ToString();
            }
        }
    }

    public class OrganizationUserAffiliation : BaseModel
    {
        public virtual UserProfile CurrentUserProfile { get; set; }
        
        public virtual Organization CurrentOrganization { get; set; }

        public OrganizationUserStatus Status { get; set; }

        public OrganizationUserRole Role { get; set; }

        public virtual ICollection<OrganizationUserNotification> NotificationHistory { get; set; }

    }

    public class OrganizationUserNotification : BaseModel
    {
        public virtual OrganizationUserAffiliation Affilitation { get; set; }

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

        public virtual UserProfile UserProfile { get; set; }

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
        SuperAdministrator,
        Administrator,
        Owner
    }

}
