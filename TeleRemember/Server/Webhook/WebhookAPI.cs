using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TeleRemember.Helper;

namespace TeleRemember.Server.Webhook
{
    public class WebhookAPI
    {
        public static async void Webhook(HttpRequest request)
        {
            JsonNode data = await HttpHelper.HttpRequestToJsonAsync(request);
        }
    }
}
