using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


using StationCAD.Model.Notifications.Clickatell;
using StationCAD.Model.Helpers;
using System.Configuration;

namespace StationCAD.Processor.Notifications
{

    public class SMS
    {
        const string CLICKATELL_APIURI = "rest/message";

        public async Task<string> CreateNotification(SMSNotification message)
        {

            string enableSMSGateway = ConfigurationManager.AppSettings["enableSMSGateway"];
            if (enableSMSGateway == null)
                throw new ApplicationException("Unable to find SMS Gateway switch");
            else if (!bool.Parse(enableSMSGateway))
                throw new ApplicationException("SMS Gateway disabled");

            string json = JsonUtil<SMSNotification>.ToJson(message);
            string result = await ClickatellAPIPost(json);

            return result;
        }

        private async Task<string> ClickatellAPIPost(string jsonBody)
        {

            string clickatellKey = ConfigurationManager.AppSettings["ClickatellKey"];
            if (clickatellKey == null)
                throw new ApplicationException("Unable to find Clickatell API Key");

            string json = string.Empty;
            using (HttpClient restClient = new HttpClient())
            {
                restClient.BaseAddress = new Uri("https://api.clickatell.com/");

                restClient.DefaultRequestHeaders.Accept.Clear();
                restClient.DefaultRequestHeaders.Add("X-Version", "1");
                restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string authHeader = string.Format("Bearer {0}", clickatellKey);
                restClient.DefaultRequestHeaders.Add("Authorization", authHeader);

                StringContent requestContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await restClient.PostAsync(CLICKATELL_APIURI, requestContent);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(string.Format("Error Occurred. Status: {0}; Headers: {1} Message: {2}", response.ReasonPhrase, response.Headers.ToString(), response.RequestMessage.ToString()));
                }

            }
            return json;        
        }
    }
}
