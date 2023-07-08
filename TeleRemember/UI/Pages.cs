using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleRemember.UI
{
    public static class Pages
    {
        public static string Menu = @"*COMMANDS*\n" + 
                                    @"/Display \\- Display all added schedules\n" +
                                    @"/Add \\- Add a new schedule";

        public static string List = @"List of all To-dos";

        public static string New0 = @"1. Enter the name of the activity.";
        public static string New1 = @"2. Enter a description. ( optional )";
        public static string New2 = @"3. Enter a link. ( optional )";
        public static string New3 = @"4. Enter the date in the format DD-MM-YYYY. ( optional )";
        public static string New4 = @"5. Priority.";
        public static string New5 = @"6. Created successfully.";

    }
}
