using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ExpenseTrackingSystem.Database.Security
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string UserGuid { get; set; }

        public virtual ICollection<Expense.Expense> Expenses { get; set; }
    }
}
