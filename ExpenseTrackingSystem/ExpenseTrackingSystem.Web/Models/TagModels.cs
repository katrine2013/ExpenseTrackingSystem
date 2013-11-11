using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackingSystem.Web.Models
{
    public class TagModels
    {
        public class CreateTagModel
        {
            public int Id { get; set; }
            
            [Required(ErrorMessage = "Please, write your login!")]
            [Display(Name = "Name")]
            public string Name { get; set; }
        }

        public class DisplayStatisticsTagModel
        {
            public int Id { get; set; }

            public decimal TotalAmountInPercentage { get; set; }

            public decimal TotalAmount { get; set; }

            public string Name { get; set; }
        }

    }
}