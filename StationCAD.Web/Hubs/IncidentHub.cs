using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace StationCAD.Web.Hubs
{
    public class IncidentHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}