using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageWeb.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Dish")]
        public int DishId { get; set; }
        [Required]
        [ForeignKey("User")]
        [Column(Order = 2)]
        public string UserToken { get; set; }
        [Required]
        [ForeignKey("User")]
        [Column(Order = 3)]
        public string UserProvider { get; set; }
        [Required]
        [Range(0,5)]
        public int Value { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual User User { get; set; }
    }
}