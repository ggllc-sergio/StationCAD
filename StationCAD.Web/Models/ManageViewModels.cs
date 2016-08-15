using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using StationCAD.Model;

namespace StationCAD.Web.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(User userInfo)
        {
            _userInfo = userInfo;
        }
        private User _userInfo = null;
        public User UserInfo
        { get { return _userInfo; } }

        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class EditViewModel
    {
        public EditViewModel(User userInfo)
        {
            _userInfo = userInfo.Profile;
            FirstName = _userInfo.FirstName;
            LastName = _userInfo.LastName;
            SecurityQuestion = _userInfo.SecurityQuestion;
            SecurityAnswer = _userInfo.SecurityAnswer;
            AccountEmail = _userInfo.AccountEmail;
            IdentificationNumber = _userInfo.IdentificationNumber;
            NotificationEmail = _userInfo.NotificationEmail;
            NotificationCellPhone = _userInfo.NotificationCellPhone;
            NotifcationPushMobile = _userInfo.NotifcationPushMobile;
            NotifcationPushBrowser = _userInfo.NotifcationPushBrowser;
        }

        private UserProfile _userInfo = null;


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
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}