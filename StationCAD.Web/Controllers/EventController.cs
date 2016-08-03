using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StationCAD.Model;
using StationCAD.Model.Helpers;
using StationCAD.Model.Notifications.Mailgun;
using StationCAD.Processor.Notifications;
using StationCAD.Processor;

namespace StationCAD.Web.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Process(FormCollection oColl)
        {
            try
            {
                DateTime eventRecieved = DateTime.Now;
                // Validate the sender
                string userIP = Request.UserHostAddress;
                string userDomain = Request.UserHostName;

                var formkeys = Request.Unvalidated.Form.Keys;
                string keystring = string.Empty;
                foreach( var item in formkeys)
                {
                    keystring += item.ToString() + ",";
                }

                string sender = Request.Unvalidated.Form["sender"];
                string body = Request.Unvalidated.Form["body-plain"];


                DispatchManager<ChesCoPAEventMessage> dispMgr = new DispatchManager<ChesCoPAEventMessage>();
                DispatchEvent eventMsg = dispMgr.ParseEventText(body);
                string json = JsonUtil<DispatchEvent>.ToJson(eventMsg);
                // do something with data 
                EmailNotification email = new EmailNotification
                {
                    Recipient = "skip513@gmail.com",
                    OrganizationName = "Lionville Fire Company",
                    MessageSubject = string.Format("Event email recieved from [{0}] @ {1}", sender, DateTime.Now),
                    MessageBody = string.Format("Keys: {1}{0}{0} User IP:{2}{0}{0} User Domain:{3}{0}{0} Json Body: {4}", Environment.NewLine,  keystring, userIP, userDomain, json)
                };
                string result = Email.SendEmailMessage(email);
            }
            catch (Exception ex)
            {

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, string.Format("Error encountered processing the event. Message: {0}", ex.Message));
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);

        }
    }
}