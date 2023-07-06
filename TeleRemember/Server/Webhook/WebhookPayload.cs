using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TeleRemember.Server.Webhook
{
    public class WebhookPayload
    {
        private JsonNode _payload;

        public WebhookPayload(
            JsonNode payload) 
        {
            _payload = payload;
        }

        public string ChatID =>
            _payload["message"]!["chat"]!["id"]!.ToString();

        public string ChatType =>
            _payload["message"]!["chat"]!["type"]!.ToString();

        public string Message =>
            _payload["message"]!["text"]!.ToString();

        public string FromUser =>
            _payload["message"]!["from"]!["username"]!.ToString();

        public string Date =>
            _payload["message"]!["data"]!.ToString();
    }
}
