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
        private readonly Controller _controller;

        public WebhookAPI(
            ILogger<WebhookAPI> logger,
            Controller controller)
        {
            _logger = logger;
            _controller = controller;
        }

        public async void Webhook(HttpRequest request)
        {
            JsonNode data = await HttpHelper.HttpRequestToJsonAsync(request);
            WebhookPayload payload = new WebhookPayload(data);

            _controller.Route(payload);
        }
    }
}
