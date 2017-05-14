using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System;

namespace GarageWeb.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "ПІБ")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Адреса")]
        public string DeliveryAddress { get; set; }

        public string Phone { get; set; }

        [Required]
        public double ToPay { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Статус замовлення")]
        public string Status { get; set; }

        public virtual ICollection<DishOrder> DishOrder { get; set; } = new List<DishOrder>();

        [NotMapped]
        public double TotalWorth => DishOrder.Sum(t => t.Dish.Price * t.Count);
    }
}