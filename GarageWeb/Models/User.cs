using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageWeb.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        [StringLength(10)]
        public string Provider { get; set; }
        [Required]
        public string Name { get; set; }
        public string AvatarUrl { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}