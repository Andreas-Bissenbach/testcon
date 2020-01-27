using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using testcon.Classes;
using System.Text;
using static testcon.Classes.WriteNotes;
using static testcon.Classes.Formation;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

namespace testcon
{
    public static class Program
    {
        private static readonly Elastic Eclient = new Elastic();
        public static int NelCVR = 10845858;
        public static int ExistingCVR = 31269741;

        public static async Task Main()
        {
            /*
 ”Vrvirksomhed.erstMaanedbeskaeftigelse” og ”Vrproduktionsenhed.erstMaanedbeskaeftigelse”, 
hvor der under disse felter være felterne ”aar”, ”maaned”,
”antalAarsvaerk”, ”antalAnsatte”, ”sidstOpdateret”, 
”intervalkodeAntalAarsvaerk” og ”intervalkodeAntalAnsatte”.
 */

            Initialize();
            string[] dsl = new string[] { DSLQuery("10845858", "Vrvirksomhed.cvrNummer"), DSLQuery("10845858", "Vrdeltagerperson.*"), DSLQuery("10845858", "VrproduktionsEnhed.*") };
            //string cvr_str = DSLQuery("30733053", "Vrvirksomhed.cvrNummer");
            string cvr_str = DSLQueryCvr("10845858");
            string x = await Eclient.ResponseData(cvr_str);
            Console.WriteLine(x.WithStringInfo());
            Console.ReadKey();
        }

        public static async Task<string[]> MultiTask(params string[] dsl)
        {
            Task<string>[] TaskArray = new Task<string>[dsl.Length];
            for (int i = 0; i < dsl.Length; i++)
            {
                TaskArray[i] = Eclient.ResponseData(dsl[i]);
            }
            return await Task.WhenAll(TaskArray);
        }

        public static async Task<string> TaskResults(params string[] dsl_queries)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var n in await MultiTask(dsl_queries))
            {
                sb.Append(n); 
            }

            return sb.ToString();
        }

        public static async Task<string> TaskFormation(string json)
        {
            StringBuilder sb = new StringBuilder();
            XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "xml_table");

            for (int i = 0; i < doc.ChildNodes.Count; i++)
            {
                string value = doc.ChildNodes.Item(i).InnerText;
                sb.Append(value);
            }

            StringReader StringStream = new StringReader(sb.ToString());


            return await StringStream.ReadToEndAsync();
        }
    }
}

