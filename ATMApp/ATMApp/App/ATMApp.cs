using ATMApp.Domain.Entities;
using ATMApp.Domain.Enums;
using ATMApp.Domain.Interfaces;
using ATMApp.UI;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.App
{
    public class ATMApp : IUserLogin, IUserAccountActions, ITransaction
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;
        private List<Transaction> _listOfTransactions;
        private const decimal minimumBalance = 20;
        private readonly AppScreen screen;

        public ATMApp()
        {
            screen = new AppScreen();
        }
        public void CheckUserCardNumAndPassword()
        {
            bool isCorrectLogin = false;
            while(isCorrectLogin == false)
            {
                UserAccount inputAccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();
                foreach(UserAccount account in userAccountList)
                {
                    selectedAccount = account;
                    if(inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                    {
                        selectedAccount.TotalLogin++;

                        if(inputAccount.CardPIN.Equals(selectedAccount.CardPIN))
                        {
                            selectedAccount = account;

                            if(selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                            {
                                AppScreen.PrintLockScreen();
                            }
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrectLogin = true;
                                break;

                            }
                        }
                    }
                    if (isCorrectLogin == false)
                    {
                        Utility.PrintMessage("\n Invalid card number or PIN.", false);
                        selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                        if (selectedAccount.IsLocked);
                        {
                            AppScreen.PrintLockScreen();

                        }
                    }
                    Console.Clear();

                }
            }           
        }
        private void ProcessMenuOption()
        {
            switch (Validator.Convert<int>("an option:"))
            {
                case (int)AppMenu.CheckBalance:
                    CheckBalance();
                    break;

                case (int)AppMenu.PlaceDeposit:
                    PlaceDeposit();
                    break;

                case (int)AppMenu.MakeWithdrawal:
                    MakeWithdrawal();
                    break;

                case (int)AppMenu.InternalTransfer:
                    var internalTransfer = screen.InternalTransferForm();
                    ProcessInternalTransfer(internalTransfer);
                    break;

                case (int)AppMenu.ViewTransaction:
                    ViewTransaction();
                    break;

                case (int)AppMenu.LogOut:
                    AppScreen.LogOutProgress();
                    Utility.PrintMessage("You have successfully logged out. Don't forget your card!");
                    Run();
                    break;

                default:
                    Utility.PrintMessage("Invalid Option.", false);
                    break;

            }
        }

        public void Run()
        {
            AppScreen.Welcome();
            CheckUserCardNumAndPassword();
            AppScreen.WelcomeCustomer(selectedAccount.FullName);
            while (true)
            {
                AppScreen.DisplayAppMenu();
                ProcessMenuOption();
            }
            
        }
        public void InitializeData()
        {
            userAccountList = new List<UserAccount>
            {
                new UserAccount {ID = 1, FullName = "Joe Accardi", AccountNumber = 123456, CardNumber = 123456, CardPIN = 133737, AccountBalance = 1500000.00m, IsLocked = false },
                new UserAccount {ID = 2, FullName = "Sal Accardi", AccountNumber = 123458, CardNumber = 654321, CardPIN = 694201, AccountBalance = 500.00m, IsLocked = false},
                new UserAccount {ID = 3, FullName = "Milo Stratton", AccountNumber = 12347, CardNumber = 456789, CardPIN = 888777, AccountBalance = 50000.00m, IsLocked = false},
            };
            _listOfTransactions = new List<Transaction>();

        }

        public void CheckBalance()
        {
            Utility.PrintMessage($"Your account balance is: {Utility.FormatAmount(selectedAccount.AccountBalance)}");
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("\nMultiples of 20 dollars only\n");
            var transactionAmount = Validator.Convert<int>($"amount {AppScreen.cur}");

            //simulate counting
            Console.WriteLine("\nCounting money...");
            Utility.PrintDotAnimation();
            Console.WriteLine("");


            if (transactionAmount <= 0)
            {
                Utility.PrintMessage("Amount needs to be greater than 0", false);
                return; 
            }
            if (transactionAmount % 20 != 0)
            {
                Utility.PrintMessage($"Enter multiples of 20.", false );
                return;

            }
            if (PreviewCashCount(transactionAmount) == false)
            
            {
                Utility.PrintMessage($"You have cancelled your action.", false);
                return;
            }

            //bind transaction details to transaction object
            InsertTransaction(selectedAccount.ID, TransactionType.Deposit, transactionAmount, "");

            //update account balance
            selectedAccount.AccountBalance += transactionAmount;

            //print success message
            Utility.PrintMessage($"You have deposited {Utility.FormatAmount(transactionAmount)}.", true);

        }

        private bool PreviewCashCount(int transactionAmount)
        {
            int hundredsCount = transactionAmount / 100;
            int twentiesCount = (transactionAmount % 100) / 20;

            Console.WriteLine("\nSummary");
            Console.WriteLine("═════════");
            Console.WriteLine($"{AppScreen.cur}100 X {hundredsCount} = {100 * hundredsCount}");
            Console.WriteLine($"{AppScreen.cur}20 X {twentiesCount} =  {20 * twentiesCount}");
            Console.WriteLine($"Total amount to deposit: {Utility.FormatAmount(transactionAmount)}\n\n");

            int opt = Validator.Convert<int>("1 to confirm");
            return opt.Equals(1);
        }

        public void MakeWithdrawal()
        {
            var transactionAmount = 0;
            int selectedAmount = AppScreen.SelectAmount();
            if(selectedAmount == -1)
            {
                selectedAmount = AppScreen.SelectAmount();

            }else if(selectedAmount != 0)
            {
                transactionAmount = selectedAmount;

            }
            else
            {
                transactionAmount = Validator.Convert<int>($"amount {AppScreen.cur}");

            }

            //input validation
            if(transactionAmount <= 0)
            {
                Utility.PrintMessage("Amount needs to be greater than zero.", false);
                return;
            }
            if(transactionAmount % 20 != 0)
            {
                Utility.PrintMessage("You can only withdraw amount in multiples of 20.", false);
                return;

            }
            //Business logic validations
            if(transactionAmount > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage($"Insufficient funds. Withdrawal failed. {Utility.FormatAmount(transactionAmount)}", false);
                return;

            }
            if ((selectedAccount.AccountBalance - transactionAmount) < minimumBalance) ;
            {
                Utility.PrintMessage($"Withdrawal failed. Your account needs a minimum balance of {Utility.FormatAmount(minimumBalance)}", false);
                return;
            }

            //bind withdrawl details to transaction object
            InsertTransaction(selectedAccount.ID, TransactionType.Withdrawal, -transactionAmount, "");
            //update account balance
            selectedAccount.AccountBalance -= transactionAmount;
            //success message
            Utility.PrintMessage($"You have successfully withdrawn {Utility.FormatAmount(transactionAmount)}", true);
        }


        public void InsertTransaction(long _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc)
        {
            //create a new transaction object
            var transaction = new Transaction()
            {
                transactionID = Utility.GetTransactionID(),
                accountID = _UserBankAccountId,
                transactionDate = DateTime.Now,
                transactionType = _tranType,
                transactionAmount = _tranAmount,
                description = _desc
            };

            //add transction object to the list
            _listOfTransactions.Add(transaction);
        }

        public void ViewTransaction()
        {
            var filteredTransactionList = _listOfTransactions.Where(t => t.accountID == selectedAccount.ID).ToList();
            //check if there's a transaction
            if(filteredTransactionList.Count <= 0)
            {
                Utility.PrintMessage("You have no transactions to view.", true);
            }
            else
            {
                var table = new ConsoleTable("ID", "Transaction Date", "Type", "Description", "Amount" + AppScreen.cur);
                foreach(var tran in filteredTransactionList)
                {
                    table.AddRow(tran.transactionID, tran.transactionDate, tran.transactionType, tran.description, tran.transactionAmount);
                }
                table.Options.EnableCount = false;
                table.Write();
                Utility.PrintMessage($"You have {filteredTransactionList.Count} transaction(s).", true);
            }
        }
        private void ProcessInternalTransfer(InternalTransfer internalTransfer)
        {
            if(internalTransfer.TransferAmount <= 0)
            {
                Utility.PrintMessage("Amount must be greater than 0.", false);
                return;

            }
        //check sender's account balance
        if(internalTransfer.TransferAmount > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage("Insufficient funds.", false);
                return;

            }
        //check the minimum balance
        if((selectedAccount.AccountBalance - minimumBalance) < minimumBalance)
            {
                Utility.PrintMessage("Insufficient funds.", false);
                return;
            }
            //check recipient account number is valid
            var recipientBankAccount = (from userAcc in userAccountList
                                        where userAcc.AccountNumber == internalTransfer.RecipientBankAccountNumber
                                        select userAcc).FirstOrDefault();
            if (recipientBankAccount == null)
            {
                Utility.PrintMessage("Invalid account number.", false);
                return;

            }
            //check receiver's name
            if(recipientBankAccount.FullName != internalTransfer.RecipientBankAccountName)
            {
                Utility.PrintMessage("Invalid account name.", false);
                return;

            }
            //add transaction to record for sender
            InsertTransaction(selectedAccount.ID, TransactionType.Transfer, -internalTransfer.TransferAmount, $"Transferred to {recipientBankAccount.AccountNumber} ({recipientBankAccount.FullName}) ");
            //update sender's account balance
            selectedAccount.AccountBalance -= internalTransfer.TransferAmount;

            //add transaction record for recipient
            InsertTransaction(recipientBankAccount.ID, TransactionType.Transfer, internalTransfer.TransferAmount, $"Transferred from {selectedAccount.AccountNumber} ({selectedAccount.FullName})");

            //update recipients account balance
            recipientBankAccount.AccountBalance += internalTransfer.TransferAmount;
            //print success message
            Utility.PrintMessage($"You have successfully transferred {Utility.FormatAmount(internalTransfer.TransferAmount)} to {internalTransfer.RecipientBankAccountName}", true);

        }



    }       
 
}
