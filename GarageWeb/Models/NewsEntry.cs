using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageWeb.Models
{
    public class NewsEntry
    {
        public NewsEntry()
        {
            _imageUrl = new Lazy<string>(GetImageUrl, true);
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не може бути порожнім")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Поле \"Опис\" не може бути порожнім")]
        [Display(Name = "Опис")]
        public string Description { get; set; }
        [Display(Name = "Зображення")]
        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Час")]
        public DateTime DateTime { get; set; }

        public void Edit(NewsEntry new_val)
        {
            Title = new_val.Title;
            Image = new_val.Image;
            Description = new_val.Description;
        }
        private string GetImageUrl()
        {
            if (Image == null) return "/Images/dish.png";
            var temp = Convert.ToBase64String(Image);
            return $"data:image;base64,{temp}";
        }

        private Lazy<string> _imageUrl;

        [NotMapped]
        public string ImageUrl => _imageUrl.Value;
    }
}