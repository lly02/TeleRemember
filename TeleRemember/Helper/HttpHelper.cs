using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeleRemember.Helper
{
    public static class HttpHelper
    {
        public async static Task<JsonNode> HttpResponseToJsonAsync(HttpResponseMessage message)
        {
            var content = await message.Content.ReadAsStringAsync();
            return StringToJson(content);
        }

        public async static Task<JsonNode> HttpRequestToJsonAsync(HttpRequest request)
        {
            using (StreamReader sr = new StreamReader(request.Body))
            {
                var content = await sr.ReadToEndAsync();
                return StringToJson(content);
            }
        }

        public static JsonNode StringToJson(string s) => 
            JsonNode.Parse(s)!;
    }
}
