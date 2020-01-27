using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Xml;
using static testcon.Classes.JsonData;
using static testcon.Classes.Controls;
using static testcon.Classes.UpdateContacts;
using RestSharp;
using static testcon.Classes.Data_Models;
using System.Web.Helpers;
using System.Collections;

namespace testcon.Classes
{
    public static class Newton
    {

        public static string GetName(JToken token)
        {
            JProperty parentProp = (JProperty)token;
            return parentProp.Name;
        }

        public static async Task ContactHandler()
        {
            string content = await ContactContent();
           
            Dictionary<KiaFields, string> KiaDictionary = new Dictionary<KiaFields, string>();
            GetContacts Contactlist = Json.Decode<GetContacts>(content);
            ArrayList custom_field = Json.Decode<GetContacts>(content).Custom_fields;

            foreach (var n in typeof(GetContacts).GetProperties())
            {
                foreach (var KiaFields in Enum.GetValues(typeof(KiaFields)).Cast<KiaFields>().ToDictionary(t => t, t => t.ToString()))
                {
                    string GetField = n.GetValue(Contactlist)?.ToString();

                    if (!string.IsNullOrEmpty(GetField) && KiaFields.Key.ToString().ToUpper() == n.Name.ToUpper())
                    {
                        if (!n.Name.ToUpper().Equals("CUSTOM_FIELDS"))
                        {
                            KiaDictionary[KiaFields.Key] = GetField;//Inserting data to the dictionary
                        }
                        else
                        {
                            string custVal = CustomDataXML(custom_field);
                            KiaDictionary[KiaFields.Key] = custVal;
                        }

                    }
                }
            }
            bool update = Contacts(KiaDictionary);
            await UpdateTask(KiaDictionary, update);
 
        }

        public static string CustomDataXML(ArrayList custom_field)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Contacts>");

            foreach (Dictionary<string, object> lists in custom_field)
            {
                sb.AppendLine("\t<Node>");
                foreach (var objects in lists)
                {
                    string val = objects.Value?.ToString();
                    string key = objects.Key?.ToString();
                    sb.AppendLine($"\t\t<{key}> {val} </{key}>");
                }
                sb.AppendLine("\t</Node>");
            }
            sb.AppendLine("</Contacts>");

            return sb.ToString();
        }

        public static string JsonToXML(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            doc = JsonConvert.DeserializeXmlNode(jsonText);
            return doc.OuterXml;
        }

        public static string JString(string response)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var n in JObject.Parse(response).Children())
            {
                sb.AppendLine(n.ToString());
            }
            return sb.ToString();
        }

        public static string SingleRecord(JToken jtoken)
        {
            Type t = jtoken.First.GetType();
            switch (t)
            {
                case Type _ when t == typeof(JValue):
                    JValue jvalue = new JValue(jtoken.ToString() + ",");
                    return jvalue.Value.ToString();

                default:
                    return "";
            }
        }

        public static string MultipleRecords(JToken jtoken)
        {
            Type t = jtoken.First.GetType();
            StringBuilder StringFromArray = new StringBuilder();
            switch (t)
            {
                case Type _ when t == typeof(JArray):
                    JArray jarray = JArray.Parse(jtoken.First.ToString());
                    foreach (var n in jarray)
                    {
                        StringFromArray.AppendLine($"{{{n.First.ToString()}}},\r\n");
                    }
                    return StringFromArray.ToString();

                default:
                    return "";
            }
        }

        public static string GetFields(string obj)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                JToken jt = JToken.Parse(obj);
                foreach (JToken jtoken in jt)
                {
                    string path = Formation.AsProperName(jtoken.Path);
                    string typename = jtoken.Path.GetType().Name;
                    string field = $"public {typename} {path} {{ get; set; }}";
                    sb.AppendLine(field);
                }
                return (sb.ToString());
            }
            catch (Exception err) when (err is JsonReaderException || err is Exception)
            {
                return err.Message;
            }
        }

        public static string JsonString(string obj, bool Single)
        {
            StringBuilder Jsb = new StringBuilder();
            try
            {
                JToken jt = JToken.Parse(obj);
                foreach (JToken jtoken in jt)
                {
                    Jsb.AppendLine(Single ? SingleRecord(jtoken) : MultipleRecords(jtoken));
                }
            }
            catch (Exception err) when (err is JsonReaderException || err is Exception)
            {
                return err.Message;
            }

            if (Jsb.Length > 3) Jsb.Remove(Jsb.Length - 3, 3);

            string[] arr = Jsb.ToString().Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);
            Jsb.Clear();

            foreach (var n in arr)
            {
                Jsb.Append(n);
            }
            string result = Single ? Formation.FormatJson("[{" + Jsb.ToString() + "}]") : Formation.FormatJson("[" + Jsb.ToString() + "]");
            return result;
        }

        public static string Debug(JToken jtoken)
        {
            Type t = jtoken.First.GetType();
            StringBuilder StringFromArray = new StringBuilder();
            switch (t)
            {
                case Type _ when t == typeof(JArray):
                    JArray jarray = JArray.Parse(jtoken.First.ToString());
                    foreach (var n in jarray)
                    {
                        StringFromArray.Append(" array-item begin " + n.ToString() + " array item end\r\n");
                    }
                    return StringFromArray.ToString();

                case Type _ when t == typeof(JObject):
                    JObject jobject = JObject.Parse(jtoken.First.ToString());
                    return jobject.First.ToString();

                case Type _ when t == typeof(JValue):
                    JValue jvalue = new JValue(" value begin " + jtoken.ToString() + " value end " + ",");
                    return jvalue.Value.ToString();

                default:
                    return $" New Type: {jtoken.GetType()} Value: {jtoken.First.ToString()}";
            }
        }
    }
}
