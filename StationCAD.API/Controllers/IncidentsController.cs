using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StationCAD.API.Controllers
{
    public class IncidentsController : ApiController
    {

        [Route("api/incidents/email")]
        public IHttpActionResult ProcessIncidentEmail([FromBody] string json)
        {

            return Ok("Email processed successfully");
        }
    }
}
