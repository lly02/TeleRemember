using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleRemember.Server.Interface
{
    public interface ITelegramAPI
    {
        public Task<HttpResponseMessage> GetMeAsync();
        public Task<HttpResponseMessage> SendMessageAsync(HttpContent httpContent);
    }
}
