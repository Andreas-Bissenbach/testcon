using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace testcon.Classes
{
    public class Client_Virk
    {
        private readonly HttpClient _httpClient;
        //http://distribution.virk.dk/
        private static readonly NetworkCredential nel_credentials = new NetworkCredential("Nellemann_Holding_CVR_I_SKYEN", "b3497fb1-17a8-49e2-8ccd-71491cad8129");
        private static readonly Uri base_addr = new Uri("http://distribution.virk.dk/cvr-permanent/_search");
        //private static readonly Uri base_addr = new Uri("https://cvrapi.dk/api");

        public Client_Virk()
        {
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
          
           _httpClient = new HttpClient(clientHandler) ?? throw new ArgumentNullException(nameof(_httpClient));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");

            clientHandler.PreAuthenticate = true;
            clientHandler.Credentials = nel_credentials;
 
            _httpClient.BaseAddress = base_addr;
        }

        public async Task<string> ResponseData(string url)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task ClearID(string scroll_id)
        {
           await _httpClient.DeleteAsync("_search/scroll/"+ scroll_id);
        }

        public async Task<string> Curl(string url)
        {
            using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), url);
            using HttpResponseMessage response = await _httpClient.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> WebRequestData(string uri)
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = "GET";
            request.Credentials = nel_credentials;
            using WebResponse response = await request.GetResponseAsync();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }

        public async Task<string> PostData(string url, string body)
        {
            using HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            string content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<string> RequestData(string url, StringContent content)
        {
            try
            {
                HttpResponseMessage msg = await _httpClient.PostAsync(url, content);
                if (msg.StatusCode == HttpStatusCode.OK)
                {
                    string response = await msg.Content.ReadAsStringAsync();
                    return response;
                }
                else
                {
                    return msg.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static async Task<StringContent> EncodedURL(Dictionary<string, string> values)
        {
            FormUrlEncodedContent getvalues = new FormUrlEncodedContent(values);
            string contenturl = await getvalues.ReadAsStringAsync();
            StringContent content = new StringContent(contenturl);
            return content;
        }
    }
}
