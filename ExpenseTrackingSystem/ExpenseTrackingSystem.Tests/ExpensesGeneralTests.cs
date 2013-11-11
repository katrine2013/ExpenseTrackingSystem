using System;
using System.Linq;
using NUnit.Framework;
using ExpenseNamespace = ExpenseTrackingSystem.Database.Expense;
using Assert = NUnit.Framework.Assert;

namespace ExpenseTrackingSystem.Tests
{
    [TestFixture]
    public class ExpensesGeneralTests : SetupData
    {
        [Test]
        public void RightCountOfExpenses()
        {
            Assert.AreEqual(3, MyAccount.Expenses.Count);
        }

        [Test]
        public void SuccessCalck_TotalAmount()
        {
            Assert.AreEqual(205, MyAccount.Expenses.Sum( x=> x.Amount ));
        }

        [Test]
        public void SuccessCalck_AmountByTag()
        {
            // add new tag
            TagsList.Add(new ExpenseNamespace.Tag()
                            {
                                Name = "party",
                                User = MyAccount
                            });

            // search expense and grup by tag
            var summary = from expense in MyAccount.Expenses
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

            // total amount value
            decimal totalAmount = MyAccount.Expenses.Sum(x => x.Amount);
            Assert.AreEqual(205, totalAmount);
            Assert.AreEqual(3, TagsList.Count);

            // amount by first tag
            Assert.AreEqual(100, summary.ToArray()[0].TotalAmount);
            Assert.AreEqual("fun", summary.ToArray()[0].Name);
            Assert.AreEqual(Math.Round(100 * 100 / totalAmount ,2), Math.Round(summary.ToArray()[0].TotalAmount * 100 / totalAmount, 2));

            // amount by second tag
            Assert.AreEqual(105, summary.ToArray()[1].TotalAmount);
            Assert.AreEqual("food", summary.ToArray()[1].Name);
            Assert.AreEqual(Math.Round(105 * 100 / totalAmount, 2), Math.Round(summary.ToArray()[1].TotalAmount * 100 / totalAmount, 2));

        }
    }
}
