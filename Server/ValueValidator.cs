using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class ValueValidator
    {
        public static void CheckPortValue(out int portValue)
        {
            while (true)
            {
                Console.Write(" Enter Port: ");

                if (int.TryParse(Console.ReadLine(), out portValue) && portValue > 49500) break;
                else
                {
                    Console.Clear();
                    Console.WriteLine(" Check your input and try again!\r\n");
                }
            }
        }

        public static void CheckIPValue(out IPAddress ipAddressValue)
        {
            while (true)
            {
                Console.Write(" Enter IP Address: ");

                if (IPAddress.TryParse(Console.ReadLine(), out ipAddressValue)) break;
                else
                {
                    Console.Clear();
                    Console.WriteLine(" Check your input and try again!\r\n");
                }

            }
        }
    }
}
