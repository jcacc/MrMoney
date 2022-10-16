using ATMApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.UI
{
    public class AppScreen
    {
        internal const string cur = "USD ";
        internal static void Welcome()
        {
            //clears the console screen
            Console.Clear();
            //sets the title of console window
            Console.Title = "Mr. Money";
            //sets text color to white
            Console.ForegroundColor = ConsoleColor.White;
            //sets welcome message
            Console.WriteLine("╒═════Mr.Money════╕");
            Console.WriteLine("|▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓▓Please insert▓▓|");
            Console.WriteLine("|▓▓▓▓▓▓your▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓▓▓▓▓▓card▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("╘══════jca════════╛");
            Utility.PressEntertoContinue();
        }
        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("your card number");
            tempUserAccount.CardPIN = Convert.ToInt32(Utility.GetSecretInput("Enter your card pin"));
            return tempUserAccount;

        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\n\nChecking card number and PIN...");
            Utility.PrintDotAnimation();

        }
        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Your account is LOCKED! Please go to the nearest branch to unlock your account.", true);
            Utility.PressEntertoContinue();
            Environment.Exit(1);
        }
        internal static void WelcomeCustomer(string fullName)
        {
            Console.WriteLine($"Welcome back, {fullName}");
            Utility.PressEntertoContinue();


        }

        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("╒═════════Mr.Money══════════╕");
            Console.WriteLine("|▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓1.) Balance▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓2.) Deposit▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓3.) Withdrawal▓▓▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓4.) Transfer▓▓▓▓▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓5.) Transactions▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓6.) Log out▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("|▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓|");
            Console.WriteLine("╘════════════jca════════════╛");
        }

        internal static void LogOutProgress()
        {
                Console.WriteLine("Thank you for using Mr. Money!");
                Utility.PrintDotAnimation();
                Console.Clear();            
        }

        internal static int SelectAmount()
        {
            Console.WriteLine("╒═════════Mr.Money══════════╕");
            Console.WriteLine("| 1.) $5       5.)$200      |");
            Console.WriteLine("| 2.) $10      6.)$400      |");
            Console.WriteLine("| 3.) $20      7.)$500      |");
            Console.WriteLine("| 4.) $50      8.)$1,000    |");
            Console.WriteLine("| 0.) Other                 |");
            Console.WriteLine("╘════════════jca════════════╛");

            int selectedAmount = Validator.Convert<int>("option:");
            switch (selectedAmount)
            {
                case 1:
                    return 5;
                    break;

                case 2:
                    return 10;
                    break;
                case 3:
                    return 20;
                    break;
                case 4:
                    return 50;
                    break;
                case 5:
                    return 200;
                    break;
                case 6:
                    return 400;
                    break;
                case 7:
                    return 500;
                    break;
                case 8:
                    return 1000;
                    break;
                case 0:
                    return 0;
                    break;
                default:
                    Utility.PrintMessage("Invalid input. Try again.", false);
                    return -1;
                    break;


            }
        }
    internal InternalTransfer InternalTransferForm()
        {
            var internalTransfer = new InternalTransfer();
            internalTransfer.RecipientBankAccountNumber = Validator.Convert<long>("recipient's account number:");
            internalTransfer.TransferAmount = Validator.Convert<decimal>($"amount {cur}");
            internalTransfer.RecipientBankAccountName = Utility.GetUserInput("recipient's name:");
            return internalTransfer;

        }
    }


}
