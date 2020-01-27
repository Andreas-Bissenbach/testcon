using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch;
using Elasticsearch.Net;
using Nest;
using System.Security;
using static testcon.Classes.VirkModels;
using System.Net;
using Elasticsearch.Net.Extensions;
using System.IO;
using System.Net.Http;

namespace testcon.Classes
{
    public class Elastic
    {
        private readonly ElasticLowLevelClient client;

        private static readonly Uri base_addr = new Uri("http://distribution.virk.dk/cvr-permanent/_search?");
        public Elastic()
        {
            SingleNodeConnectionPool pool = new SingleNodeConnectionPool(base_addr);
            ConnectionSettings connectionSettings = new ConnectionSettings(pool).RequestTimeout(TimeSpan.FromMinutes(2));
            connectionSettings.DefaultIndex("_doc");
            connectionSettings.BasicAuthentication("Nellemann_Holding_CVR_I_SKYEN", "b3497fb1-17a8-49e2-8ccd-71491cad8129");
            connectionSettings.DisableDirectStreaming(true);
            
            client = new ElasticLowLevelClient(connectionSettings) ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> ResponseData(string search)
        {
            StringResponse searchResponse = await client.SearchAsync<StringResponse>(search);

            bool successful = searchResponse.Success;
            string responseJson = searchResponse.Body;
            
            return successful ? responseJson : searchResponse.ApiCall.DebugInformation;
        }

    }

}
