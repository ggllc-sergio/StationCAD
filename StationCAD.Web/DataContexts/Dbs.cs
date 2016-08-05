﻿using Microsoft.AspNet.Identity.EntityFramework;
using StationCAD.Web.Models;

namespace StationCAD.Web.DataContexts
{
    public class IdentityDb1 : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb1()
            : base("StationCAD_Web", throwIfV1Schema: false)
        {
        }

        public static IdentityDb1 Create()
        {
            return new IdentityDb1();
        }
    }


}