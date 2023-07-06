using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TeleRemember.Helper;

namespace TeleRemember.Server.Webhook
{
    public class WebhookAPI
    {
        private readonly ILogger _logger;
        private readonly Bot _bot;

        public WebhookAPI(
            ILogger<WebhookAPI> logger,
            Bot bot)
        {
            _logger = logger;
            _bot = bot;
        }

        public async void Webhook(HttpRequest request)
        {
            JsonNode data = await HttpHelper.HttpRequestToJsonAsync(request);
            WebhookPayload payload = new WebhookPayload(data);

            _bot.ProcessPayload(payload);
        }
    }
}
