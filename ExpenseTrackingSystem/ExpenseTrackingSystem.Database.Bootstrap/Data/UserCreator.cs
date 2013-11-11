using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseTrackingSystem.Database.Security;

namespace ExpenseTrackingSystem.Database.Bootstrap.Data
{
    public static class UserCreator
    {
        public static void CreateUsers(ExpenseDatabaseContext context)
            {
                var user = new User()
                               {
                                   Login = "kate",
                                   Password = "111",
                                   UserGuid = Guid.NewGuid().ToString()
                               };
                context.Users.Add(user);
            }
    }
}
