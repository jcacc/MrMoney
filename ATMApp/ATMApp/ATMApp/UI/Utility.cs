using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.UI
{
    public static class Utility
    {
        private static long transactionID;
        private static CultureInfo culture = new CultureInfo("en-US");

        public static long GetTransactionID()
        {
            return ++transactionID;

        }
        public static string GetSecretInput(string prompt)
        {
            bool isPrompt = true;
            string astericks = "";

            StringBuilder input = new StringBuilder();

            while (true)
            {
                if (isPrompt)
                    Console.WriteLine(prompt);
                isPrompt = false;
                ConsoleKeyInfo inputKey = Console.ReadKey(true);
                
                if(inputKey.Key == ConsoleKey.Enter)
                {
                    if(input.Length == 6)
                    {
                        break;
                    }
                    else
                    {
                        PrintMessage("\n\nPlease enter 6 digits.", false);
                        input.Clear();
                        isPrompt = true;
                        continue;
                        
                    }
                }
                if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                }else if(inputKey.Key != ConsoleKey.Backspace)
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(astericks + "*");

                }
            }
            return input.ToString();
         }

        public static void PrintMessage(string msg, bool success = true)
        {
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            PressEntertoContinue();

        }

        public static string GetUserInput(string prompt)
        {
            Console.WriteLine($"Enter {prompt}");
            return Console.ReadLine();
        }

        public static void PrintDotAnimation(int timer = 10)
        {
            for (int i = 0; i < timer; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);

            }
            Console.Clear();
        }

        public static void PressEntertoContinue()
        {
            Console.WriteLine("\n\n═════Press enter to continue══════\n\n");
            Console.ReadLine();
        }

        public static string FormatAmount(decimal amt)
        {
            return String.Format(culture, "{0:C2}", amt);
        }
    
    
    }
     



}
