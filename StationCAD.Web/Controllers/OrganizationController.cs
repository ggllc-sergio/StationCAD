using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StationCAD.Web.Business;
using StationCAD.Model;
using StationCAD.Model.DataContexts;

namespace StationCAD.Web.Controllers
{
    public class OrganizationController : BaseController
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> UploadUsers(OrganizationUserRegistrationUpload orgUserReg)
        {

            List<UserRegistration> userErrors = new List<UserRegistration>();
            OrgUserRegistration orgReg = new OrgUserRegistration();
            orgReg.Organization = new Organization();
            orgReg.Users = FileService.ParseFileStream(orgUserReg.File.InputStream);
            userErrors = await ImportUsers(orgUserReg);

            return View(userErrors);
        }

        private async Task<List<UserRegistration>> ImportUsers(OrgUserRegistration orgUserReg)
        {
            List<UserRegistration> userErrors = new List<UserRegistration>();
            using (var db = new StationCADDb())
            {
                // Loop thru....
                foreach (UserRegistration item in orgUserReg.Users)
                {
                    User user = null;
                    try
                    {
                        // 1. Check to see if the user exists
                        user = db.Users.Include("Profile").Where(x => x.Email == item.Email).FirstOrDefault();

                        // 1b. if not, CreateAsync() for User
                        if (user == null)
                        {
                            user = new User { Id = Guid.NewGuid().ToString(), UserName = item.Email, Email = item.Email };  
                            DateTime now = DateTime.Now;
                            user.Profile = new UserProfile { FirstName = item.FirstName, LastName = item.LastName, CreateUser = "", CreateDate = now, LastUpdateUser = "", LastUpdateDate = now };
                            var result = await UserManager.CreateAsync(user, "P@ssword1");
                            if (!result.Succeeded)
                            {
                                userErrors.Add(item);
                                break;
                            }
                        }
                        // add user-org-affiliation
                        OrganizationUserAffiliation uoa = new OrganizationUserAffiliation();
                        uoa.CurrentOrganization = orgUserReg.Organization;
                        uoa.Role = OrganizationUserRole.User;
                        uoa.Status = OrganizationUserStatus.Active;
                        user.Profile.OrganizationAffiliations = new List<OrganizationUserAffiliation>();
                        user.Profile.OrganizationAffiliations.Add(uoa);
                        await db.SaveChangesAsync();
                    }
                    catch(Exception ex)
                    {
                        string msg = string.Format("", "");
                        base.LogException("", ex);
                        userErrors.Add(item);
                        break;
                    }
                    // 3. Create Email confirmation
                    // 3a. If !emailConfirmed, create emailConfirmToken and send email
                    if (!user.EmailConfirmed)
                    {
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ResetPassword", "Account",
                            new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        string body = string.Format("{1}, {0}{0}  You have been added as a user to {2}. Please confirm your account by clicking <a href=\"{3}\">here</a>.  You will be required to choose a new password. {0}{0} Thanks, {0}{0} The StationCAD Team",
                            Environment.NewLine,
                            user.Profile.FirstName,
                            orgUserReg.Organization.Name,
                            callbackUrl);
                        await UserManager.SendEmailAsync(user.Id, "StationCAD - Activate your account", body);
                    }
                    // 3b. Just send email letting them know they have been added to the org
                    else
                    {
                        string body = string.Format("{1}, {0}{0}  You have been added as a user to {2}. {0}{0} Thanks, {0}{0} The StationCAD Team",
                            Environment.NewLine,
                            user.Profile.FirstName,
                            orgUserReg.Organization.Name);
                        await UserManager.SendEmailAsync(user.Id, "StationCAD - You are part of a new Organization!", body);
                    }                    
                }
            }
            return userErrors;
        }
    }
}