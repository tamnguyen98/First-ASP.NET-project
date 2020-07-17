using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLibrary.Models
{
    public class OrderModel
    {
        public int Id { get; set; }


        // Optional Annotitaion, should prefered in interface
        [Required]
        [MaxLength(20, ErrorMessage = "Name max length is 20")]
        [MinLength(3, ErrorMessage = "Name length >= 3")]
        [DisplayName("Name for the Order")]
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [DisplayName("Meal")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a meal from the list")]
        public int FoodId { get; set; }

        [Required]
        [Range(1,10, ErrorMessage = "Max quantity is 10")]
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
