using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcon.Classes
{
    public class Client_Model
    {
        public static string APIKEY { get; } = "c0f7fcbd4bfb453c9c862d7e5ca94dbc";

        private readonly RestClient _httpClient;

        public Client_Model(RestClient httpClient)
        {
            httpClient.BaseUrl = new Uri("https://api2.autopilothq.com/v1/");
            httpClient.AddDefaultHeader("autopilotapikey", APIKEY);
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> ResponseData(string req_uri)
        {
            RestRequest request = new RestRequest(req_uri);
            IRestResponse response = await _httpClient.ExecuteTaskAsync(request);
            return response.Content;
        }

        public async Task<string> PostData(string req_uri)
        {
            RestRequest request = new RestRequest(req_uri);
            IRestResponse response = await _httpClient.ExecutePostTaskAsync(request);
            return response.Content;
        }
    }
}
