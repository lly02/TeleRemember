using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using TeleRemember.Helper;
using TeleRemember.Server.Interface;
using TeleRemember.Server.Webhook;
using TeleRemember.UI;

namespace TeleRemember.Server
{
    public class Bot
    {
        private readonly Config _config;
        private readonly ILogger _logger;
        private readonly string _baseUrl;
        private readonly HttpSharedClient _httpClient;
        private readonly ITelegramAPI _telegramAPI;

        public Bot(
            Config config,
            HttpSharedClient httpSharedClient,
            ILogger<Bot> logger,
            ITelegramAPI telegramAPI)
        {
            _config = config;
            _logger = logger;
            _baseUrl = "https://api.telegram.org/bot" + _config.BotToken + "/";
            _httpClient = httpSharedClient;
            _telegramAPI = telegramAPI;

            _httpClient.SetBaseUri(_baseUrl);
        }

        public async Task InitAsync()
        {
            _logger.LogInformation("Begin bot initialization.");

            await IsBotValid();
        }

        private async Task<bool> IsBotValid()
        {
            HttpResponseMessage result = await _telegramAPI.GetMeAsync();
            JsonNode data = await HttpHelper.HttpResponseToJsonAsync(result);

            if (!(bool)data["ok"]!)
            {
                _logger.LogError("Invalid bot token {}", _config.BotToken);
                throw new UnauthorizedAccessException("Invalid bot token.");
            }

            _logger.LogInformation("Bot is valid {_config._BotToken}", _config.BotToken);
            return true;
        }
    }
}
