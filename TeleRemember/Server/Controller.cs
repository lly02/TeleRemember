using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleRemember.Helper;
using TeleRemember.Server.Interface;
using TeleRemember.Server.Payload;
using TeleRemember.Server.Webhook;
using TeleRemember.UI;

namespace TeleRemember.Server
{
    public class Controller
    {
        private readonly ILogger _logger;
        private readonly ITelegramAPI _telegramAPI;
        private readonly Model _model;

        private WebhookPayload? _currentPayload;
        private string _currentPage = "menu";
        private int _newStep = 0;
        private NewPayload? _newPayload;

        public Controller(
            Model model,
            ILogger<Controller> logger,
            ITelegramAPI telegramApi) 
        {
            _model = model;
            _logger = logger;
            _telegramAPI = telegramApi;
        }

        public void Route(WebhookPayload payload)
        {
            _currentPayload = payload;

            if (_currentPayload.ChatType != "group")
            {
                _logger.LogInformation("The bot currently only replies in a group.");
                return;
            }

            if (BotHelper.IsCommand(_currentPayload.Message))
            {
                string command = BotHelper.GetCommand(_currentPayload.Message);

                if (command == "")
                {
                    _logger.LogInformation("Invalid command: {message}", _currentPayload.Message);
                    return;
                }

                _logger.LogInformation("Command received: {message}", command);

                UserInput(command.ToLower());
            }
            else
            {
                UserInput(_currentPage, _currentPayload.Message);
            }

        }

        private void UserInput(string action, string message = "")
        {
            switch (action)
            {
                case "menu":
                    DisplayMenu(message);
                    break;

                case "new":
                    DisplayNew(message);
                    break;

                case "display":
                    DisplayList();
                    break;
            }
        }

        private async void DisplayMenu(string message = "")
        {
            string content = $$"""
            {
                "chat_id": {{_currentPayload!.ChatID}},
                "text": "{{message}}{{Pages.Menu}}",
                "parse_mode": "MarkdownV2",
                "disable_web_page_preview": false,
                "reply_markup": {{Markup.Menu}}
            }
            """;
            var httpContent = HttpHelper.StringToHttpResponse(content);
            HttpResponseMessage httpResponse = await _telegramAPI.SendMessageAsync(httpContent);

            if (!httpResponse.IsSuccessStatusCode)
            {
                await Error(httpResponse);
            }

            _currentPage = "menu";
        }

        private async void DisplayNew(string message = "")
        {
            string content = "";

            switch (_newStep)
            {
                case 0:
                    _newPayload = new NewPayload();
                    content = $$"""
                    {
                        "chat_id": {{_currentPayload!.ChatID}},
                        "text": "{{Pages.New0}}",
                        "parse_mode": "MarkdownV2",
                        "disable_web_page_preview": false
                    }
                    """;
                    _newStep++;
                    break;

                case 1:
                    _newPayload!.Title = message;
                    content = $$"""
                    {
                        "chat_id": {{_currentPayload!.ChatID}},
                        "text": "{{Pages.New1}}",
                        "parse_mode": "MarkdownV2",
                        "disable_web_page_preview": false
                    }
                    """;
                    _newStep++;
                    break;

                case 2:
                    _newPayload!.Description = message;
                    content = $$"""
                    {
                        "chat_id": {{_currentPayload!.ChatID}},
                        "text": "{{Pages.New2}}",
                        "parse_mode": "MarkdownV2",
                        "disable_web_page_preview": false
                    }
                    """;
                    _newStep++;
                    break;

                case 3:
                    _newPayload!.Link = message;
                    content = $$"""
                    {
                        "chat_id": {{_currentPayload!.ChatID}},
                        "text": "{{Pages.New3}}",
                        "parse_mode": "MarkdownV2",
                        "disable_web_page_preview": false
                    }
                    """;
                    _newStep++;
                    break;

                case 4:
                    try
                    {
                        _newPayload!.Due = DateTime.Parse(message);
                    } catch (Exception e)
                    {
                        Error(e.Message);
                    }
                    content = $$"""
                    {
                        "chat_id": {{_currentPayload!.ChatID}},
                        "text": "{{Pages.New4}}",
                        "parse_mode": "MarkdownV2",
                        "disable_web_page_preview": false,
                        "reply_markup": {{Markup.Priority}}
                    }
                    """;
                    _newStep++;
                    break;

                case 5:
                    _newPayload!.Priority = message;
                    _newPayload!.CreatedBy = _currentPayload!.FromUser;
                    _newPayload!.CreatedDate = DateTime.Now;
                    try
                    {
                        await _model.InsertTodo(_newPayload);
                    } catch (Exception e)
                    {
                        Error(e.Message);
                    }
                    content = $$"""
                    {
                        "chat_id": {{_currentPayload!.ChatID}},
                        "text": "{{Pages.New5}}",
                        "parse_mode": "MarkdownV2",
                        "disable_web_page_preview": false,
                        "reply_markup": {{Markup.Success}}
                    }
                    """;
                    _newStep = 0;
                    _newPayload = null;
                    break;
            }

            var httpContent = HttpHelper.StringToHttpResponse(content);
            HttpResponseMessage httpResponse = await _telegramAPI.SendMessageAsync(httpContent);

            if (!httpResponse.IsSuccessStatusCode)
            {
                await Error(httpResponse);
            }

            _currentPage = "new";
        }

        private async void DisplayList()
        {
            var list = await _model.GetAllTodo();
            string items = ""; 
            foreach (var item in list)
            {
                items += $$"""
                [
                    {
                        "text" : "Title: {{item.Title}}\nDue: {{item.Due}}\n Priority: {{item.Priority}}\n{{item.Link}}"
                    }
                ]
                """;
            }

            string markup = $$"""
            {
                "inline_keyboard": [
            """ + items + $$$"""
                ]
            }
            """;

            string content = $$"""
            {
                "chat_id": {{_currentPayload!.ChatID}},
                "text": "{{Pages.List}}",
                "parse_mode": "MarkdownV2",
                "disable_web_page_preview": false,
                "reply_markup": {{markup}}
            }
            """;
            var httpContent = HttpHelper.StringToHttpResponse(content);
            HttpResponseMessage httpResponse = await _telegramAPI.SendMessageAsync(httpContent);

            if (!httpResponse.IsSuccessStatusCode)
            {
                await Error(httpResponse);
            }

            _currentPage = "display";
        }

        private async Task Error(HttpResponseMessage response)
        {
            var result = await HttpHelper.HttpResponseToJsonAsync(response);
            Error(result["description"]!.ToString());
            _newStep = 0;
        }

        private void Error(string e)
        {
            _logger.LogError("{errorMessage}", e);
            UserInput("menu", @"*Error\\!*" + "\n\n");
        }
    }
}
