using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageWeb.Models
{
    public class Dish
    {
        public Dish()
        {
            _imageUrl = new Lazy<string>(GetImageUrl,true);
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле \"Назва\" не може бути порожнім")]
        [StringLength(50)]
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле \"Вага\" не може бути порожнім")]
        [Display(Name = "Вага порції")]
        public float Weight { get; set; }
        [Required(ErrorMessage = "Поле \"Ціна\" не може бути порожнім")]
        [Display(Name = "Ціна")]
        public float Price { get; set; }
        [Display(Name = "Зображення")]
        [MaxLength]
        public byte[] Image { get; set; }
        [Display(Name = "Опис")]
        public string Description { get; set; }
        [StringLength(20)]
        [Display(Name = "Категорія")]
        public string Category { get; set; } = "Main";
       
        
        public virtual ICollection<DishOrder> DishOrder { get; set; } = new List<DishOrder>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        private string GetImageUrl()
        {
            if (Image == null) return "/Images/dish.png";
            var temp = Convert.ToBase64String(Image);
            return $"data:image;base64,{temp}";
        }
        private Lazy<string> _imageUrl;
        [NotMapped]
        public string ImageUrl => _imageUrl.Value;
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