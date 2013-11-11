using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseTrackingSystem.Database.Expense;

namespace ExpenseTrackingSystem.Database.Bootstrap.Data
{
    public static class ExpenseCreator
    {
        public static void CreateExpenses(ExpenseDatabaseContext context)
        {
            var tag = new Tag()
                          {
                              Name = "food"
                          };
            context.Tags.Add(tag);

            var expense = new Expense.Expense();
            expense.Amount = 20;
            expense.DateOfMakingExpense = DateTime.UtcNow;
            expense.Description = "in the market";
            expense.Tag = tag;
            expense.User = context.Users.FirstOrDefault();
            context.Expenses.Add(expense);

            tag = new Tag()
                      {
                          Name = "party"
                      };
            context.Tags.Add(tag);

            expense = new Expense.Expense();
            expense.Amount = (Decimal)136.7;
            expense.DateOfMakingExpense = DateTime.UtcNow;
            expense.Description = "birthday";
            expense.Tag = tag;
            expense.User = context.Users.FirstOrDefault();
            context.Expenses.Add(expense);
        }
    }
}
