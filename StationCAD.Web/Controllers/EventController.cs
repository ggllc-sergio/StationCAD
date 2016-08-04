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
using System.Text;

namespace StationCAD.Web.Controllers
{
    public class EventController : BaseController
    {
        private readonly string _byteOrderMarkUtf8 =
                Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Process(FormCollection oColl)
        {
            HttpStatusCodeResult httpResult;
            string sender = string.Empty;
            string body = string.Empty;
            string attachmentData = string.Empty;
            string userIP = string.Empty;
            string userDomain = string.Empty;
            string keystring = string.Empty;
            string json = string.Empty;
            string err = string.Empty;
            string uvFileCnt = string.Empty;
            string vFileCnt = string.Empty;
            int fileLen = 0;
            byte[] fileContent = new byte[0];
            string attachmentName = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sender = Request.Unvalidated.Form["sender"];
                body = Request.Unvalidated.Form["body-plain"];
                DateTime eventRecieved = DateTime.Now;
                // Validate the sender
                userIP = Request.UserHostAddress;
                userDomain = Request.UserHostName;
                sb.AppendLine(string.Format("Keys:{0} {1}{0}{0}", Environment.NewLine, keystring));
                sb.AppendLine(string.Format("User IP:{1}{0}{0}", Environment.NewLine, userIP));
                sb.AppendLine(string.Format("User Domain:{1}{0}{0}", Environment.NewLine, userDomain));

                var formkeys = Request.Unvalidated.Form.Keys;
                foreach (var item in formkeys)
                {
                    keystring += item.ToString() + ",";
                }
                vFileCnt = Request.Files.Count.ToString();
                uvFileCnt = Request.Unvalidated.Files.Count.ToString();
                sb.AppendLine(string.Format("Attachment Count: {1}, {2}{0}{0} ", Environment.NewLine, vFileCnt, uvFileCnt));
                if (Request.Unvalidated.Files.Count > 0)
                {
                    // for this example; processing just the first file
                    HttpPostedFileBase file = Request.Unvalidated.Files[0];
                    fileLen = file.ContentLength;
                    attachmentName = file.FileName;
                    sb.AppendLine(string.Format("Length:{0}{1}{0}{0}", Environment.NewLine, fileLen));
                    if (fileLen >= 0)
                    {
                        // throw an error here if content length is not > 0
                        // you'll probably want to do something with file.ContentType and file.FileName
                        fileContent = new byte[file.ContentLength];
                        file.InputStream.Read(fileContent, 0, file.ContentLength);
                        sb.AppendLine(string.Format("File Content Length:{0}{1}{0}{0}", Environment.NewLine, fileContent.Length));
                        // fileContent now contains the byte[] of your attachment...
                        attachmentData = System.Text.Encoding.Default.GetString(fileContent);
                        sb.AppendLine(string.Format("Attachment Data:{0}{1}{0}{0}", Environment.NewLine, attachmentData));
                    }
                }

                DispatchManager dispMgr = new DispatchManager();
                DispatchEvent eventMsg;
                if (attachmentData.Length > 0)
                {
                    eventMsg = dispMgr.ParseEventHtml(attachmentData);
                    eventMsg.FileName = attachmentName;
                }
                else
                    eventMsg = dispMgr.ParseEventText(body);
                json = JsonUtil<DispatchEvent>.ToJson(eventMsg);
                sb.AppendLine(string.Format("Json Body:{0} {1}{0}{0}", Environment.NewLine, json));
                httpResult = new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                err = ex.ToString();
                sb.AppendLine(string.Format("Error:{0}{1}{0}{0}", Environment.NewLine, err));
                httpResult = new HttpStatusCodeResult(HttpStatusCode.InternalServerError, string.Format("Error encountered processing the event. Message: {0}", ex.Message));
            }
            finally
            {
                // do something with data 
                EmailNotification email = new EmailNotification
                {
                    Recipient = "skip513@gmail.com",
                    OrganizationName = "Lionville Fire Company",
                    MessageSubject = string.Format("Event email recieved from [{0}] @ {1}", sender, DateTime.Now),
                    MessageBody = sb.ToString()
                };
                string result = Email.SendEmailMessage(email);
            }
            return httpResult;
        }
        
    }
}