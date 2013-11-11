using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ExpenseTrackingSystem.Database.Expense
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        //Expense sum
        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }
        
        // date of making expense
        [Required]
        public DateTime DateOfMakingExpense { get; set; }

        // choosen tag
        [Required]
        public virtual Tag Tag { get; set; }

        // owner
        [Required]
        public virtual Security.User User { get; set; }
    }
}
