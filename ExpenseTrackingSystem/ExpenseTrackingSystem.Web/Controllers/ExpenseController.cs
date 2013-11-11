using System;
using System.Web.Mvc;
using ExpenseTrackingSystem.Web.Logic;
using ExpenseTrackingSystem.Web.Models;

namespace ExpenseTrackingSystem.Web.Controllers
{
    public class ExpenseController : Controller
    {
        //
        // GET: /Expense/

        [Authorize]
        public ActionResult ShowExpenses()
        {
            // request info about expense
            var extenseHelper = new ExpenseGeneralFunction();
            var allUserExtenses = extenseHelper.AllUserExpenses(User.Identity.Name, DateTime.UtcNow.AddDays(-7),
                                                                               DateTime.UtcNow);
            // total amount
            decimal totalAmount = 0;

            // cut expense by tag
            var statTags = extenseHelper.ExpenseByTag(allUserExtenses, ref totalAmount);

            // return model with statistic
            return View(new ExpenseModels.DisplayStatisticModel()
                            {
                                StatisticsByTag = statTags,
                                AllExpenses = allUserExtenses,
                                TotalAmount = totalAmount,
                                BeginOfPeriod = DateTime.Now.AddDays(-7),
                                EndOfPeriod = DateTime.Now
                            });
        }

        /// <summary>
        /// request expense by the period
        /// </summary>
        /// <param name="model">request model</param>
        /// <returns>result model</returns>
        [Authorize]
        [HttpPost]
        public ActionResult ShowExpenses(ExpenseModels.RequestStatisticModel model)
        {
            // select all Expenses
            var extenseHelper = new ExpenseGeneralFunction();
            var allUserExtenses = extenseHelper.AllUserExpenses(User.Identity.Name, model.BeginOfPeriod.ToUniversalTime(),
                                                                               model.EndOfPeriod.ToUniversalTime());
            // spent sum
            decimal totalAmount = 0;

            // create info by tags
            var statTags = extenseHelper.ExpenseByTag(allUserExtenses, ref totalAmount);

            // create new model, that contain all needing data
            return View(new ExpenseModels.DisplayStatisticModel()
            {
                StatisticsByTag = statTags,
                AllExpenses = allUserExtenses,
                TotalAmount = totalAmount,
                BeginOfPeriod = model.BeginOfPeriod,
                EndOfPeriod = model.EndOfPeriod
            });
        }

        [Authorize]
        public ActionResult NewExpense()
        {
            return View(new ExpenseModels.CreateExpenseModel(User.Identity.Name));
        }

        /// <summary>
        /// save new expense
        /// </summary>
        /// <param name="model">saving data</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult NewExpense(ExpenseModels.SaveExpenseModel model)
        {
            if (ModelState.IsValid)
            {
                if (new ExpenseGeneralFunction().AddExpense(model, User.Identity.Name) == Enums.Messages.Good)
                {
                    ViewBag.Mess = "Expense added";

                    return View(new ExpenseModels.CreateExpenseModel(User.Identity.Name));
                }
            }

            return View(new ExpenseModels.CreateExpenseModel()
            {
                DateOfMakingExpense = model.DateOfMakingExpense,
                Amount = model.Amount,
                Description = model.Description,
                Tags = new TagGeneralFunction().AllUserTags(User.Identity.Name),
                Tag = model.Tag
            });
        }

        /// <summary>
        /// Delete expense
        /// </summary>
        /// <param name="Id">record Id</param>
        /// <returns>result message</returns>
        [Authorize]
        [HttpPost]
        public Enums.Messages DeleteExpense(int Id)
        {
            return new ExpenseGeneralFunction().DeleteExpense(Id, User.Identity.Name);
        }

        /// <summary>
        /// Edit some expense
        /// </summary>
        /// <param name="model">saving data</param>
        /// <param name="NewTag">new tag name</param>
        /// <returns>result</returns>
        [Authorize]
        [HttpPost]
        public ActionResult EditExpense (ExpenseModels.DisplayExpenseModel model, string NewTag)
        {
            new ExpenseGeneralFunction().EditExpense(model, NewTag, User.Identity.Name);

            return RedirectToAction("ShowExpenses", "Expense");
        }
    }
}
