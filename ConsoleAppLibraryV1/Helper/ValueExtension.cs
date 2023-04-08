using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibraryV1.Helper
{
    public static partial class Extension
    {
        static public int ReadInteger(string caption, bool checkInterval = false, int minValue = 0, int maxvalue = 0)
        {
            int value;

        l1:
            Console.Write(caption);

            if (!int.TryParse(Console.ReadLine(), out value))
            {
                goto l1;
            }

            if (checkInterval && (value < minValue || value > maxvalue))
            {
                Console.WriteLine($"{value} bu intervalda deyil [{minValue}, {maxvalue}]");
                goto l1;
            }

            return value;
        }
        static public ushort ReadUInt16(string caption, bool checkInterval = false, int minValue = 0, int maxvalue = 0)
        {
            ushort value;

        l1:
            Console.Write(caption);

            if (!ushort.TryParse(Console.ReadLine(), out value))
            {
                goto l1;
            }

            if (checkInterval && (value < minValue || value > maxvalue))
            {
                Console.WriteLine($"{value} bu intervalda deyil [{minValue}, {maxvalue}]");
                goto l1;
            }

            return value;
        }


        static public decimal ReadDecimal(string caption, bool checkInterval = false, decimal minValue = 0)
        {
            decimal value;

        l1:
            Console.Write(caption);

            if (!decimal.TryParse(Console.ReadLine(), out value))
            {
                goto l1;
            }

            if (checkInterval && value < minValue)
            {
                Console.WriteLine($"{value} minimum {minValue} ola biler!");
                goto l1;
            }

            return value;
        }


        static public string ReadString(string caption)
        {
            Console.Write(caption);

            return Console.ReadLine();
        }
    }
}
