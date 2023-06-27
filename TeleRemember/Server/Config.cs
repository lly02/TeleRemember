using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace TeleRemember.Server
{
    public class Config
    {
        public string BotToken { get; private set; }

        private IConfiguration _config;

        public Config(
            IConfiguration config)
        {
            _config = config;

            BotToken = GetConfig("botToken");
        }

        private string GetConfig(string key) => 
            _config.GetValue<string>(key) ?? throw new NullReferenceException(key + "is missing");
    }
}
