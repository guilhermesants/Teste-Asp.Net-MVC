using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace PrimeiraQuestaoAvonale.Service
{
    public class GetUri
    {
        public static async System.Threading.Tasks.Task<HttpResponseMessage> Uri(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

                return await client.GetAsync(client.BaseAddress.ToString());
            }
        }
    }
}