using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ExpenseTrackingSystem.Database.Bootstrap.Data;

namespace ExpenseTrackingSystem.Database.Bootstrap
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // drop exists database and create new
                System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<ExpenseDatabaseContext>());

                using (var context = new ExpenseDatabaseContext())
                {
                    // create new user
                    UserCreator.CreateUsers(context);
                    context.SaveChanges();
                    //create new expense
                    ExpenseCreator.CreateExpenses(context);
                    context.SaveChanges();
                }

                Console.WriteLine("OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }
    }
}
