using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GarageWeb.Models
{
    public class NewsEntry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public string GetImageUrl()
        {
            if (Image == null) return "/Images/dish.png";
            var temp = Convert.ToBase64String(Image);
            return $"data:image;base64,{temp}";
        }
    }
}