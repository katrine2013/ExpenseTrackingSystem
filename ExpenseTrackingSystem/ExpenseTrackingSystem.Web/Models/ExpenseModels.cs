using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ExpenseTrackingSystem.Database.Expense;
using ExpenseTrackingSystem.Web.Logic;

namespace ExpenseTrackingSystem.Web.Models
{
    public class ExpenseModels
    {
        public class CreateExpenseModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Please, write date")]
            [DataType(DataType.DateTime)]
            [Display(Name = "DateOfMakingExpense")]
            public DateTime DateOfMakingExpense { get; set; }

            [Required(ErrorMessage = "Please, write sum")]
            [DataType(DataType.Currency)]
            [Display(Name = "Amount")]
            public decimal Amount { get; set; }

            [Display(Name = "Description")]
            public string Description { get; set; }

            // collection of the tag
            public IEnumerable<TagModels.CreateTagModel> Tags { get; set; }

            // current value
            public int Tag { get; set; }

            public CreateExpenseModel() {}

            public CreateExpenseModel(string userIdentity)
            {
                // select all user's tags
                Tags = new TagGeneralFunction().AllUserTags(userIdentity).ToList();
                // date - today
                DateOfMakingExpense = DateTime.Now;
                // current Tag - first record
                Tag = Tags.Any() ? Tags.First().Id : 0;
            }
        }

        public class SaveExpenseModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Please, write date")]
            [DataType(DataType.DateTime)]
            [Display(Name = "DateOfMakingExpense")]
            public DateTime DateOfMakingExpense { get; set; }

            [Required(ErrorMessage = "Please, write sum")]
            [DataType(DataType.Currency)]
            [Display(Name = "Amount")]
            [Range(0, 9999999, ErrorMessage = "Please, check bill's value")]
            public decimal Amount { get; set; }

            [Display(Name = "Description")]
            public string Description { get; set; }

            [Required(ErrorMessage = "Please, choose tag")]
            [DataType(DataType.Text)]
            [Display(Name = "Tag")]
            public int Tag { get; set; }
        }

        /// <summary>
        /// model for showing all user expenses
        /// </summary>
        public class DisplayExpenseModel
        {
            public int Id { get; set; }

            // date of the expense
            public DateTime DateOfMakingExpense { get; set; }

            // sum in bill
            public decimal Amount { get; set; }

            // explanation
            public string Description { get; set; }

            // tag of the expense
            public Tag Tag { get; set; }
        }

        public class DisplayStatisticModel
        {
            // start of the period
            public DateTime BeginOfPeriod { get; set; }

            // end of the period
            public DateTime EndOfPeriod { get; set; }

            // collection of the making expenses
            public IEnumerable<DisplayExpenseModel> AllExpenses { get; set; }

            // statistic using tags
            public IEnumerable<TagModels.DisplayStatisticsTagModel> StatisticsByTag { get; set; }

            // was spent for the period
            public decimal TotalAmount { get; set; }
        }

        public class RequestStatisticModel
        {
            // start of the period
            public DateTime BeginOfPeriod { get; set; }

            // end of the period
            public DateTime EndOfPeriod { get; set; }
        }
    }
}