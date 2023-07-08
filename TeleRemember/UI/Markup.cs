using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleRemember.UI
{
    public static class Markup
    {
        public static string Menu = $$"""
            {
                "keyboard":
                    [
                        [ 
                            {
                                "text": "/Display"
                            }
                        ],
                        [
                            {
                                "text" : "/New"
                            }
                        ]
                    ],
                "is_persistent" : true,
                "resize_keyboard": true,
                "one_time_keyboard" : true
            }
            """;

        public static string Priority = $$"""
            "keyboard":
                [
                    [ 
                        {
                            "text": "Low"
                        }
                    ],
                    [
                        {
                            "text" : "Medium"
                        }
                    ],
                    [
                        {
                            "text" : "High"
                        }
                    ]
                ],
            "is_persistent" : true,
            "resize_keyboard": true,
            "one_time_keyboard" : true
        """;

        public static string Success = $$"""
            "keyboard":
                [
                    [ 
                        {
                            "text": "/Menu"
                        }
                    ]
                ],
            "is_persistent" : true,
            "resize_keyboard": true,
            "one_time_keyboard" : true
        """;
    }
}
