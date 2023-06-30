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

        public WebhookAPI(
            ILogger<WebhookAPI> logger)
        {
            _logger = logger;
        }

        public async void Webhook(HttpRequest request)
        {
            JsonNode data = await HttpHelper.HttpRequestToJsonAsync(request);
            JsonNode chat = data["message"]!["chat"]!;
            string message = data["message"]!["text"]!.ToString();

            if (chat!["type"]!.ToString() != "group")
            {
                _logger.LogInformation("The bot currently only replies in a group.");
                return;
            }

            if (!BotHelper.IsCommand(message)) return;

            string? command = BotHelper.GetCommand(message);

            if (command == "")
            {
                _logger.LogInformation("Invalid command: {message}", message);
                return;
            }

            _logger.LogInformation("Command received: {message}", command);
        }
    }
}
