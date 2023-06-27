using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleRemember.Server.Interface;

namespace TeleRemember.Server
{
    public class TelegramAPI : ITelegramAPI
    {
        private HttpClient _httpClient;
        public TelegramAPI(
            HttpSharedClient httpSharedClient) 
        { 
            _httpClient = httpSharedClient.HttpClient;
        }

        public async Task<HttpResponseMessage> GetMeAsync() => 
            await _httpClient.GetAsync("getMe");
            
    }
}
