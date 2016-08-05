using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using StationCAD.Model;
using StationCAD.Model.Helpers;
using StationCAD.Model.Notifications.OneSignal;
using System.Configuration;

namespace StationCAD.Processor.Notifications
{
    public class Push
    {
        const string ONESIGNAL_APPID = "950a66d5-c457-4c9e-9a8b-cca7ef8b8c68";

        #region Devices 

        /// GET OneSignal 'Players' (devices)
        /// https://onesignal.com/api/v1/players?app_id=:app_id&limit=:limit&offset=:offset
        /// 
        public async Task<DeviceList> GetDevices()
        {
            DeviceList results = null;
            string api = string.Format("api/v1/players?app_id={0}", ONESIGNAL_APPID);
            string json = await OneSignalAPIGet(api);

            if (json.Length > 0)
                try { 
                results = JsonUtil<DeviceList>.FromJson(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }

            return results;
        }

        /// GET OneSignal 'Player by ID' (device)
        /// https://onesignal.com/api/v1/players/:id
        /// 
        public async Task<Device> GetDevice(UserProfile user)
        {
            Device results = null;
            string api = string.Format("api/v1/players/{0}", user.IdentificationNumber);
            string json = await OneSignalAPIGet(api);

            if (json.Length > 0)
                try { 
                results = JsonUtil<Device>.FromJson(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }

            return results;
        }

        /// POST OneSignal 'Update Player' (device)
        /// https://onesignal.com/api/v1/players/:id
        /// 
        public async Task<string> UpdateDevice(DeviceEdit device)
        {
            string json = JsonUtil<DeviceEdit>.ToJson(device);
            string api = string.Format("api/v1/players/{0}", device.Id);
            string result = await OneSignalAPIPut(api, json);

            return result;
        }

        #endregion

        #region Notifications 

        /// GET OneSignal 'Notifications'
        /// https://onesignal.com/api/v1/notifications?app_id={appId}
        /// 
        public async Task<PushNotificationList> GetNotifications()
        {
            PushNotificationList results = null;
            string api = string.Format("api/v1/notifications?app_id={0}", ONESIGNAL_APPID);
            string json = await OneSignalAPIGet(api);

            if (json.Length > 0)
                try {
                    results = JsonUtil<PushNotificationList>.FromJson(json);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }
            return results;
        }


        /// GET OneSignal 'Notification by Id'
        /// https://onesignal.com/api/v1/notifications/{notificationId}?app_id={appId}
        /// 
        public async Task<PushNotification> GetNotification(string notificationId)
        {
            PushNotification results = null;
            string api = string.Format("api/v1/notifications/{0}?app_id={1}", notificationId, ONESIGNAL_APPID);
            string json = await OneSignalAPIGet(api);

            if (json.Length > 0)
                try { 
                results = JsonUtil<PushNotification>.FromJson(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                }

            return results;
        }

        /// POST OneSignal 'Create Notification'
        /// https://onesignal.com/api/v1/notifications
        /// 
        public async Task<string> CreatNotification(PushNotificationCreate push)
        {
            push.ApplicationId = ONESIGNAL_APPID;
            string json = JsonUtil<PushNotificationCreate>.ToJson(push);
            string api = "api/v1/notifications";
            string result = await OneSignalAPIPost(api, json);

            return result;
        }

        #endregion

        private async Task<string> OneSignalAPIGet(string api)
        {
            string oneSignalKey = ConfigurationManager.AppSettings["OneSignalAPIKey"];
            if (oneSignalKey == null)
                throw new ApplicationException("Unable to find OneSignal API Key");
            
            string json = string.Empty;
            using (HttpClient restClient = new HttpClient())
            {
                restClient.BaseAddress = new Uri("https://onesignal.com/");

                restClient.DefaultRequestHeaders.Accept.Clear();
                restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string authHeader = string.Format("Basic {0}", oneSignalKey);
                restClient.DefaultRequestHeaders.Add("Authorization", authHeader);
                
                HttpResponseMessage response = await restClient.GetAsync(api);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(string.Format("Error Occurred. Status: {0}; Headers: {1} Message: {2}", response.ReasonPhrase, response.Headers.ToString(), response.Content));
                }

            }
            return json;
        }

        private async Task<string> OneSignalAPIPost(string api, string jsonBody)
        {

            string oneSignalKey = ConfigurationManager.AppSettings["OneSignalAPIKey"];
            if (oneSignalKey == null)
                throw new ApplicationException("Unable to find OneSignal API Key");

            string json = string.Empty;
            using (HttpClient restClient = new HttpClient())
            {
                restClient.BaseAddress = new Uri("https://onesignal.com/");

                restClient.DefaultRequestHeaders.Accept.Clear();
                restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string authHeader = string.Format("Basic {0}", oneSignalKey);
                restClient.DefaultRequestHeaders.Add("Authorization", authHeader);
                
                StringContent requestContent = new StringContent(jsonBody, Encoding.UTF8, "application/json"); 

                HttpResponseMessage response = await restClient.PostAsync(api, requestContent);
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
        
        private async Task<string> OneSignalAPIPut(string api, string jsonBody)
        {

            string oneSignalKey = ConfigurationManager.AppSettings["OneSignalAPIKey"];
            if (oneSignalKey == null)
                throw new ApplicationException("Unable to find OneSignal API Key");

            string json = string.Empty;
            using (HttpClient restClient = new HttpClient())
            {
                restClient.BaseAddress = new Uri("https://onesignal.com/");

                restClient.DefaultRequestHeaders.Accept.Clear();
                restClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string authHeader = string.Format("Basic {0}", oneSignalKey);
                restClient.DefaultRequestHeaders.Add("Authorization", authHeader);

                StringContent requestContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await restClient.PutAsync(api, requestContent);
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
