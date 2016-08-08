using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StationCAD.Web.Models;
using StationCAD.Model;
using StationCAD.Model.DataContexts;

namespace StationCAD.Web.Controllers
{
    [Authorize]
    public class IncidentController : BaseController
    {
        
        public IncidentController() { }


        public async Task<ActionResult> Index(int orgID)
        {
            Organization orgModel = new Organization();
            using (var db = new StationCADDb())
            {
                orgModel = db.Organizations
                    .Include("Addresses")
                    .Include("IncidentHistory")
                    .Where(x => x.Id == )
            }
            return View(orgModel);
        }



    }
}
