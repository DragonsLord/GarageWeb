using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageWeb.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public float Price { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }
        [StringLength(20)]
        public string Category { get; set; } = "Main";

        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public string GetImageUrl()
        {
            if (Image == null) return "/Images/dish.png";
            var temp = Convert.ToBase64String(Image);
            return $"data:image;base64,{temp}";
        }
        [NotMapped]
        public double CurrentRating
        {
            get
            {
                if (Ratings.Count == 0) return 0;
                return Ratings.Average(r => r.Value);
            }
        }
        public void Edit(Dish new_val)
        {
            Name = new_val.Name;
            Weight = new_val.Weight;
            Price = new_val.Price;
            Image = new_val.Image;
            Description = new_val.Description;
            Category = new_val.Category;
        }
    }
}