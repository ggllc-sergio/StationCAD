using Microsoft.AspNet.Identity.EntityFramework;
using StationCAD.Model;
using StationCAD.Web.Models;

namespace StationCAD.Web.DataContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("StationCAD_Web", throwIfV1Schema: false)
        {
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
    }


}