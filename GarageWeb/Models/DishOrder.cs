using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GarageWeb.Models
{
    public class DishOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("Dish")]
        public int DishId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Order Order { get; set; }

        public virtual Dish Dish { get; set; } 
    }
}