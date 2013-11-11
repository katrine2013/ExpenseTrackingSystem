using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ExpenseTrackingSystem.Database.Expense;
using ExpenseTrackingSystem.Database.Security;

namespace ExpenseTrackingSystem.Database
{
    public class ExpenseDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Expense.Expense> Expenses { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
