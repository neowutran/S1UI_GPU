using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivateGPUS1UI
{
    internal static class ConsoleOutput
    {
        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            JustMessage(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void InformationMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            JustMessage(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void StandartMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static void JustMessage(string message)
        {
            Console.WriteLine(String.Format("[{0}]:{1}", DateTime.Now, message));
        }
    }
}
