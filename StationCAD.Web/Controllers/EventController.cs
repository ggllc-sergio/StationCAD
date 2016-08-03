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
using System.IO;

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
            string sender = Request.Unvalidated.Form["sender"];
            string body = Request.Unvalidated.Form["body-plain"];
            string attachmentData = string.Empty;
            string userIP = string.Empty;
            string userDomain = string.Empty;
            string keystring = string.Empty;
            string json = string.Empty;
            string err = string.Empty;
            string uvFileCnt = string.Empty;
            string vFileCnt = string.Empty;
            try
            {
                DateTime eventRecieved = DateTime.Now;
                // Validate the sender
                userIP = Request.UserHostAddress;
                userDomain = Request.UserHostName;

                var formkeys = Request.Unvalidated.Form.Keys;
                foreach (var item in formkeys)
                {
                    keystring += item.ToString() + ",";
                }
                vFileCnt = Request.Files.Count.ToString();
                uvFileCnt = Request.Unvalidated.Files.Count.ToString();
                if (Request.Unvalidated.Files.Count > 0)
                {
                    // for this example; processing just the first file
                    HttpPostedFileBase file = Request.Unvalidated.Files[0];
                    if (file.ContentLength == 0)
                    {
                        // throw an error here if content length is not > 0
                        // you'll probably want to do something with file.ContentType and file.FileName
                        byte[] fileContent = new byte[file.ContentLength];
                        file.InputStream.Read(fileContent, 0, file.ContentLength);
                        // fileContent now contains the byte[] of your attachment...
                        attachmentData = System.Text.Encoding.Default.GetString(fileContent);
                    }
                }

                DispatchManager<ChesCoPAEventMessage> dispMgr = new DispatchManager<ChesCoPAEventMessage>();
                DispatchEvent eventMsg = dispMgr.ParseEventText(body);
                json = JsonUtil<DispatchEvent>.ToJson(eventMsg); }
            catch (Exception ex)
            {
                err = ex.ToString();
                //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, string.Format("Error encountered processing the event. Message: {0}", ex.Message));
            }
            // do something with data 
            EmailNotification email = new EmailNotification
            {
                Recipient = "skip513@gmail.com",
                OrganizationName = "Lionville Fire Company",
                MessageSubject = string.Format("Event email recieved from [{0}] @ {1}", sender, DateTime.Now),
                MessageBody = string.Format("Keys:{0} {1}{0}{0} User IP:{2}{0}{0} User Domain:{3}{0}{0} Json Body:{0} {4}{0}{0} Attachment[ Count - {7}, {8}]:{0}  {5}{0}{0} Error:{0} {6}", 
                            Environment.NewLine,  keystring, userIP, userDomain, json, attachmentData, err, vFileCnt, uvFileCnt)
            };
            string result = Email.SendEmailMessage(email);
        

            return new HttpStatusCodeResult(HttpStatusCode.OK);

        }
    }
}