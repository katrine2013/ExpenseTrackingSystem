using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseTrackingSystem.Database;
using ExpenseTrackingSystem.Database.Expense;
using ExpenseTrackingSystem.Web.Models;

namespace ExpenseTrackingSystem.Web.Logic
{
    public class ExpenseGeneralFunction
    {
        /// <summary>
        /// Search all user's expense
        /// </summary>
        /// <param name="userIdentity">User's identtety info</param>
        /// <returns>Expense list</returns>
        public IEnumerable<ExpenseModels.DisplayExpenseModel> AllUserExpenses(string userIdentity, DateTime beginPeriod,
                                                                              DateTime endPeriod)
        {
            var resultCollection = new List<ExpenseModels.DisplayExpenseModel>();

            using (var context = new ExpenseDatabaseContext())
            {
                var expensesList = context.Expenses.Include("Tag").Where(x => x.User.Login == userIdentity
                                                                              &&
                                                                              DateTime.Compare(beginPeriod,
                                                                                               x.DateOfMakingExpense) <=
                                                                              0
                                                                              &&
                                                                              DateTime.Compare(endPeriod,
                                                                                               x.DateOfMakingExpense) >=
                                                                              0
                    );
                foreach (Expense expense in expensesList.AsQueryable())
                {
                    var displayModel = new ExpenseModels.DisplayExpenseModel
                                           {
                                               Id = expense.ExpenseId,
                                               DateOfMakingExpense = expense.DateOfMakingExpense.ToLocalTime(),
                                               Amount = expense.Amount,
                                               Description = expense.Description,
                                               Tag = expense.Tag
                                           };
                    resultCollection.Add(displayModel);
                }
            }

            return resultCollection.OrderBy(x => x.DateOfMakingExpense);
        }

        /// <summary>
        /// Create statistic by tags
        /// </summary>
        /// <param name="userExpenses">all expenses</param>
        /// <param name="totalAmount">total amount sum</param>
        /// <returns>Statisctic by tags</returns>
        public IEnumerable<TagModels.DisplayStatisticsTagModel> ExpenseByTag(
            IEnumerable<ExpenseModels.DisplayExpenseModel> userExpenses, ref decimal totalAmount)
        {
            var resultCollection = new List<TagModels.DisplayStatisticsTagModel>();

            var summary = from expense in userExpenses
                          let tagSta = new
                                           {
                                               Amount = expense.Amount,
                                               Name = expense.Tag.Name
                                           }
                          group expense by expense.Tag
                          into t
                          select new
                                     {
                                         Name = t.Key.Name,
                                         TotalAmount = t.Sum(p => p.Amount)
                                     };

            totalAmount = summary.Sum(x => x.TotalAmount);

            foreach (var statistic in summary)
            {
                resultCollection.Add(new TagModels.DisplayStatisticsTagModel()
                                         {
                                             Name = statistic.Name,
                                             TotalAmount = statistic.TotalAmount,
                                             TotalAmountInPercentage =
                                                 Math.Round(statistic.TotalAmount*100/totalAmount, 2)
                                         });
            }

            return resultCollection;
        }

        /// <summary>
        /// Add new tag
        /// </summary>
        /// <param name="expense">Expense saving model</param>
        /// <param name="userIdentity">Owner</param>
        /// <returns>Result message</returns>
        public Enums.Messages AddExpense(ExpenseModels.SaveExpenseModel expense, string userIdentity)
        {
            using (var context = new ExpenseDatabaseContext())
            {
                //find tag. You can edit just yours tags
                var tag = context.Tags.FirstOrDefault(x => x.TagId == expense.Tag);
                var user = context.Users.FirstOrDefault(x => x.Login == userIdentity);
                // check user account
                if (user == null)
                {
                    return Enums.Messages.UserNotRegistrate;
                }
                // new expense will add
                if (tag != null)
                {
                    context.Expenses.Add(new Expense()
                                             {
                                                 Amount = expense.Amount,
                                                 DateOfMakingExpense = expense.DateOfMakingExpense.ToUniversalTime(),
                                                 Description = expense.Description,
                                                 Tag = tag,
                                                 User = user
                                             });

                    return context.SaveChanges() >= 1 ? Enums.Messages.Good : Enums.Messages.ErorWriteInDatabase;
                }

                return Enums.Messages.ErorWriteInDatabase;
            }
        }

        /// <summary>
        /// Delete expense
        /// </summary>
        /// <param name="id">id record</param>
        /// <param name="userIdentity">user account</param>
        /// <returns>result message</returns>
        public Enums.Messages DeleteExpense(int id, string userIdentity)
        {
            using (var context = new ExpenseDatabaseContext())
            {
                //find expense. You can edit just yours expenses
                var expense = context.Expenses.FirstOrDefault(x => x.ExpenseId == id && x.User.Login == userIdentity);
                if (expense != null)
                {
                    context.Expenses.Remove(expense);

                    return context.SaveChanges() >= 1 ? Enums.Messages.Good : Enums.Messages.ErorWriteInDatabase;
                }

                return Enums.Messages.RecordNotFound;
            }
        }

        /// <summary>
        /// Edit expense
        /// </summary>
        /// <param name="model">edit model</param>
        /// <param name="userIdentity">user account</param>
        /// <returns>result message</returns>
        public Enums.Messages EditExpense(ExpenseModels.DisplayExpenseModel model, string NewTag, string userIdentity)
        {
            using (var context = new ExpenseDatabaseContext())
            {
                // find expense. You can edit just yours expenses
                var expense = context.Expenses.FirstOrDefault(x => x.ExpenseId == model.Id && x.User.Login == userIdentity);
                // seach new tag
                var tag = context.Tags.FirstOrDefault(x => !x.IsDelete && x.Name == NewTag);
                if (expense != null)
                {
                    // if tag didn't find , than create new tag
                    if (tag == null)
                    {
                        tag = (new Tag()
                                   {
                                       Name = NewTag,
                                       User = expense.User
                                   });
                        context.Tags.Add(tag);
                    }

                    // save data
                    expense.Amount = model.Amount;
                    expense.Tag = tag;
                    expense.Description = model.Description;
                    expense.DateOfMakingExpense = model.DateOfMakingExpense;

                    return context.SaveChanges() >= 1 ? Enums.Messages.Good : Enums.Messages.ErorWriteInDatabase;
                }

                return Enums.Messages.RecordNotFound;
            }
        }
    }
}