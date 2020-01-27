using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace testcon.Classes
{
    public static class ClientCalls
    {
        private static readonly JsonData Data = new JsonData(new HttpClient());

        public static async Task<string> ContactInfo()
        {
            return (await Data.ResponseData("contacts"));
        }

        public static async Task<string> AccountInfo()
        {
            return (await Data.ResponseData("account"));
        }

        public static async Task<string> GetHooks()
        {
            return (await Data.ResponseData("hooks"));
        }

        public static async Task<string> CustomResponse(string req_url)
        {
            return (await Data.ResponseData(req_url));
        }

        public static async Task<Type> Types(string req_url)
        {
            return (await Data.ObjectType(req_url));
        }

        public static async Task<string> RegisterHook(string event_name,string target_url)
        {
            event_name = "\"" + event_name + "\"";
            target_url = "\"" + target_url + "\"";

            using StringContent content = new StringContent("{  \"event\": " + event_name + ",  \"target_url\": " + target_url + "}", Encoding.Default, "application/json");
            string post = await Data.PostData("hook", content);
            return ("Hook Added: " + post);
        }

        public static async Task<string> UnregisterHook(string hook_id)
        {
            string Delete = await Data.DeleteData($"hook/{hook_id}");
            return ($"Hook with id: {hook_id} was removed {Delete}");
        }

        public static async Task<string> UnregisterAllHooks()
        {
            string Delete = await Data.DeleteData("hooks");
            return (Delete);
        }

        public static async Task<string> AddContact(string mail)
        {
            mail = "\"" + mail + "\"";
            using StringContent content = new StringContent("{  \"contact\": {    \"FirstName\": \"Andreas\",    \"LastName\": \"Petersen\",    \"Email\": "+mail+ ",    \"custom\": {      \"string--Test--Field\": \"No field\"   }  }}", Encoding.Default, "application/json");
            
            string Add = await Data.PostData("contact", content);
            return Add;
        }

        public static async Task<string> UpdateContact(string FirstName, string mail)
        {
            mail = "\"" + mail + "\"";
            FirstName = "\"" + FirstName + "\"";
            using StringContent content = new StringContent("{  \"contact\": {    \"FirstName\": " + FirstName + ",    \"LastName\": \"Petersen\",    \"Email\": " + mail + ",    \"custom\": {      \"string--Test--Field\": \"This is a test\"    }  }}", Encoding.Default, "application/json");

            string Update = await Data.PostData("contact", content);
            return Update;
        }

        public static async Task<string> DeleteContact(string contact_id_or_email)
        {
            using StringContent content = new StringContent("");

            string Delete = await Data.PostData($"contact/{contact_id_or_email}/unsubscribe", content);
            return Delete;
        }

        public static async Task<string> DeleteMultipleContacts(string contact_id_or_email)
        {
         
            string Delete = await Data.DeleteData($"contact/{contact_id_or_email}");
            return $"{contact_id_or_email} was deleted {Delete}";
        }

        public static string HookIDExample { get; } = "hook_contact_added_7c433a84cc9a6d6a34ef41c1e2a15d1d89ad5191";
        public static string TargetUrlExample { get; } = "https://hooks.zapier.com/hooks/standard/3255825/6d6ca950298242d1ba850860b1b06823";
        public static string EventNameExample { get; } = "contact_added";
    }
}
