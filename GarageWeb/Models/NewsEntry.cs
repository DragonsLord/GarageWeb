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
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Опис")]
        public string Description { get; set; }
        [Display(Name = "Зображення")]
        public byte[] Image { get; set; }

        public void Edit(NewsEntry new_val)
        {
            Title = new_val.Title;
            Image = new_val.Image;
            Description = new_val.Description;
        }
        public string GetImageUrl()
        {
            if (Image == null) return "/Images/dish.png";
            var temp = Convert.ToBase64String(Image);
            return $"data:image;base64,{temp}";
        }
    }
}