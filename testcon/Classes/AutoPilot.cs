using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using testcon.Classes;
using static testcon.Classes.UpdateContacts;
using static testcon.Classes.JsonData;

namespace testcon.Classes
{
    public static class AutoPilot
    {
        public static int CountFields = 0;
        public static int CountResults = 0;
        public static int CountResultSets = 0;
        public static JsonData Data = new JsonData(new HttpClient());

    }

    public class ContactList
    {
        public List<Dictionary<string, object>> Contacts { get; set; }
        public object Bookmark { get; set; }
    }

    public class JsonData 
    {
        public enum KiaFields
        {
            Name = 0, LastName = 1, FirstName = 2, Email = 3, created_at = 4, updated_at = 5,
            Status = 6, LeadSource = 7, contact_id = 8, first_visit_at = 9, lastTimezone = 10,
            lastLocation = 11, lastEngagement = 12, MailingPostalCode = 13, Phone = 14, Company = 15,
            latestSubscribe = 16, company_priority = 17, unsubscribed = 18, api_originated = 19
        };

        public static string APIKEY { get; } = "c0f7fcbd4bfb453c9c862d7e5ca94dbc";

        private readonly HttpClient _httpClient;
        public JsonData(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://api2.autopilothq.com/v1/");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("autopilotapikey", APIKEY);
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task ContactValues(string s)
        {
            Dictionary<KiaFields, string> KiaDictionary = new Dictionary<KiaFields, string>();
            using HttpResponseMessage response = await _httpClient.GetAsync(s);
            string content = await response.Content.ReadAsStringAsync();
            ContactList Contactlist = Json.Decode<ContactList>(content);
            
            foreach (var list in Contactlist.Contacts)
            {
                foreach (var contacts in list.Where(s => s.Value.GetType() == typeof(string) || s.Value.GetType() == typeof(long) || s.Value.GetType() == typeof(bool)))
                {
                    foreach (var KiaFields in Enum.GetValues(typeof(KiaFields)).Cast<KiaFields>().ToDictionary(t => t, t => t.ToString()))
                    {
                        if (KiaFields.Key.ToString().Equals(contacts.Key))
                        {
                            KiaDictionary[KiaFields.Key] = contacts.Value.ToString();//Inserting data to the dictionary

                            if (contacts.Key.Equals("contact_id"))
                            {
                                bool updateKia = Contacts(KiaDictionary);
                                await UpdateTask(KiaDictionary, updateKia);
                                AutoPilot.CountResults++;
                                await Newton.ContactHandler();
                            }
                        }
                    }
                }
            }

            AutoPilot.CountResultSets++;
            GetBookmark = SetBookmark(content);

        }

        public async Task<string> ResponseData(string s)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(s);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<object> UnknownResponse(string s)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(s);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PostData(string req_uri, HttpContent content)
        {
            using var response = await _httpClient.PostAsync(req_uri, content);
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }

        public async Task<string> DeleteData(string req_uri)
        {
            using var response = await _httpClient.DeleteAsync(req_uri);
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }

        public static string SetBookmark(string content)
        {
            return Json.Decode<ContactList>(content).Bookmark.ToString();
        }

        public static string GetBookmark { get; set; }



        public async Task<Type> ObjectType(string s)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(s);
            string content = await response.Content.ReadAsStringAsync();

            
            return Json.Decode<object>(content).GetType();
        }
    }
}
