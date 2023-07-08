using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TeleRemember.Helper
{
    public static class BotHelper
    {
        public static bool IsCommand(string message) =>
            message[0] == '/';

        public static string GetCommand(string message)
        {
            var pattern = @"^\/(\w+)@*";
            var match = Regex.Match(message, pattern);

            return match.Groups[1].Value;
        }

    }
}
