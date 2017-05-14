using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GarageWeb.Models
{
    public class DishOrder
    {
        [Key, Column(Order = 1)]
        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Key, Column(Order = 2)]
        [Required]
        [ForeignKey("Dish")]
        public int DishId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Order Order { get; set; }

        public virtual Dish Dish { get; set; } 
    }
}