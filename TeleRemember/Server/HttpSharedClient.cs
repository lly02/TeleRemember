using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleRemember.Server
{
    public class HttpSharedClient
    {
        public readonly HttpClient HttpClient;

        public HttpSharedClient()
        {
            HttpClient = new HttpClient();
        }

        public void SetBaseUri(string uri)
        {
            HttpClient.BaseAddress = new Uri(uri);
        }
    }
}
