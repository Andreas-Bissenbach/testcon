using Segment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Segment.Model;
using System.Data.SqlClient;
using System.Net.Http;

namespace testcon.Classes
{
    public static class Controls
    {
        public static string ContactID = string.Empty;
        public static string GetXML = string.Empty;
        private static readonly JsonData Json = new JsonData(new HttpClient());

        public static async Task<string> ContactContent()
        {
           
            return await ClientCalls.CustomResponse($"contact/{ContactID}");

        }

        public async static Task DataInsertion()
        {
            try
            {
                while (true)
                {
                    await Json.ContactValues($"contacts/{JsonData.GetBookmark}");



                }
            }
            catch (Exception err) when (err is KeyNotFoundException || err is ArgumentNullException || err is FormatException || err is NullReferenceException || err is SqlException)
            {

                if (err.Message.Contains("Object reference"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("All bookmarks has been handled, contact update is concluded");
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: "+err.Message);
                    Console.ReadKey();
                }
            }
        }
    }
}
