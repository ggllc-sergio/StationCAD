using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using RestSharp;
using RestSharp.Authenticators;

using StationCAD.Model.Notifications.Mailgun;

namespace StationCAD.Processor.Notifications
{
    public class Email
    {
        public string SendAPIMessage(EmailNotification email)
        {

            string mailGunKey = ConfigurationManager.AppSettings["mailGunKey"];
            if (mailGunKey == null)
                throw new ApplicationException("Unable to find MailGun API Key");
            string mailGunDomain = ConfigurationManager.AppSettings["mailGunDomain"];
            if (mailGunDomain == null)
                throw new ApplicationException("Unable to find MailGun Domain Key");

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", mailGunKey);

            RestRequest request = new RestRequest();
            request.AddParameter("domain", mailGunDomain, ParameterType.UrlSegment);
            request.Resource = string.Format("{0}/messages", mailGunDomain);

            request.AddParameter("from", string.Format("{0} <{1}@{2}>", email.OrganizationName, email.OrganizationEmail, mailGunDomain));
            request.AddParameter("to", email.Recipient);
            request.AddParameter("subject", email.MessageSubject);
            request.AddParameter("text", email.MessageBody);
            request.Method = Method.POST;

            IRestResponse result = client.Execute(request);

            return result.StatusCode.ToString();
        }

        
    }
}
