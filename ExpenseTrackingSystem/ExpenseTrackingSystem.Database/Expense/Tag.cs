using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ExpenseTrackingSystem.Database.Expense
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        public string Name { get; set; }

        // 0 - not dalete record
        // 1 - record were deleted
        public bool IsDelete { get; set; }

        // tag owner
        // Null - general tags. Such tags will be see all users
        public virtual Security.User User { get; set; }
    }
}
