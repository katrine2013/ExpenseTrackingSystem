using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ExpenseTrackingSystem.Database.Security;
using ExpenseNamespace = ExpenseTrackingSystem.Database.Expense;

namespace ExpenseTrackingSystem.Tests
{
    public class SetupData
    {
        protected User MyAccount;
        protected List<ExpenseNamespace.Tag> TagsList;

        public SetupData()
        {
            // create new account record
            MyAccount = new User()
                            {
                                Login = "katrine",                      // login
                                Password = "123456",                    // passwor
                                UserGuid = Guid.NewGuid().ToString(),   // generate new GUID
                                Expenses = new Collection<ExpenseNamespace.Expense>()   // empty collection of expenes
                            };

            TagsList = new List<ExpenseNamespace.Tag>();

            // create 1-st tag
            ExpenseNamespace.Tag tagFun = new ExpenseNamespace.Tag()
                                           {
                                               Name = "fun",
                                               User = MyAccount
                                           };
            TagsList.Add(tagFun);

            // create new expense
            ExpenseNamespace.Expense expense1 = new ExpenseNamespace.Expense()
                                         {
                                             Amount = 100,                            // expense sum
                                             DateOfMakingExpense = DateTime.UtcNow,   // date of take expense  
                                             Description = "present for mom",         // reason
                                             Tag = tagFun,                            // select tag
                                             User = MyAccount                         // owner
                                         };

            ExpenseNamespace.Tag tagFood = new ExpenseNamespace.Tag()
                                        {
                                            Name = "food",
                                            User = MyAccount
                                        };

            TagsList.Add(tagFood);

            MyAccount.Expenses.Add(expense1);

            ExpenseNamespace.Expense expense2 = new ExpenseNamespace.Expense()
                                        {
                                            Amount = 50,
                                            DateOfMakingExpense = DateTime.UtcNow,
                                            Description = "",
                                            Tag = tagFood,
                                            User = MyAccount
                                        };

            MyAccount.Expenses.Add(expense2);

            ExpenseNamespace.Expense expense3 = new ExpenseNamespace.Expense()
            {
                Amount = 55,
                DateOfMakingExpense = DateTime.UtcNow,
                Description = "",
                Tag = tagFood,
                User = MyAccount
            };

            MyAccount.Expenses.Add(expense3);

        }
    }
}
