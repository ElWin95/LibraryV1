using ConsoleAppLibraryV1.StableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibraryV1.Helper
{
    public partial class Extension
    {
        public static Menu PrintMenu()
        {
            ConsoleColor tempColor = Console.ForegroundColor;

            Type type = typeof(Menu);
            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"=========== MENU ===========");
            foreach (var item in Enum.GetValues(type))
            {
                Console.WriteLine($"{((int)item).ToString().PadLeft(2, '0')}. {item}");
            }
            Console.WriteLine($"============================");
            Console.ForegroundColor = tempColor;

        l1: Console.Write("rejimi secin: ");

            if (!Enum.TryParse<Menu>(Console.ReadLine(), out Menu selectedMenu)
                || !Enum.IsDefined(type, selectedMenu))
            {
                goto l1;
            }

            return selectedMenu;
        }

        public static T ReadEnum<T>(string caption)
            where T : Enum
        {
            ConsoleColor tempColor = Console.ForegroundColor;

            Type type = typeof(T);
            Type underlineType = Enum.GetUnderlyingType(type);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"=========== {type.Name} ===========");
            foreach (var item in Enum.GetValues(type))
            {
                Console.WriteLine($"{Convert.ChangeType(item, underlineType)?.ToString().PadLeft(2, '0')}. {item}");
            }
            Console.WriteLine($"============================");
            Console.ForegroundColor = tempColor;

        l1: Console.Write(caption);
            string enumStr = Console.ReadLine();

            if (!Enum.TryParse(type, enumStr, out object value))
            {
                goto l1;
            }

            if (!Enum.IsDefined(type, value))
            {
                goto l1;
            }

            return (T)value;
        }
    }
}
