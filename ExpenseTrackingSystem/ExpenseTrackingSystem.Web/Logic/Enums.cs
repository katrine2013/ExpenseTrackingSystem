using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTrackingSystem.Web.Logic
{
    public class Enums
    {
        public enum Messages
        {
            Good,
            EmptyMessage,
            UserNotRegistrate,
            RecordNotFound,
            ErrorReadDatabase,
            ErorWriteInDatabase,
            ErrorAccessPermission
        }
    }
}