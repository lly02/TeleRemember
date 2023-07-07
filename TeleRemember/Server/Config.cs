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
        public string BotToken 
        { 
            get => GetConfig<string>("Telegram:BotToken");
        }
        public string ConnectionString
        {
            get => GetConfig<string>("ConnectionStrings:SqlServer");
        }

        private IConfiguration _config;

        public Config(
            IConfiguration config)
        {
            _config = config;
        }

        private T GetConfig<T>(string key) => 
            _config.GetValue<T>(key) ?? throw new NullReferenceException(key + " is missing.");
    }
}
