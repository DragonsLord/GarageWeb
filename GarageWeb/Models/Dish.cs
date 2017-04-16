﻿using System;
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

        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public string GetImageUrl()
        {
            var temp = Convert.ToBase64String(Image);
            return $"data:image;base64,{temp}";
        }
        [NotMapped]
        public double CurrentRating
        {
            get
            {
                return Ratings.Average(r => r.Value);
            }
        }
    }
}