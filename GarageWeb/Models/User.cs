using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace GarageWeb.Models
{
    public class User
    {
        [Key]
        [Column(Order = 0)]
        public string Token { get; set; }
        [Key]
        [StringLength(10)]
        [Column(Order = 1)]
        public string Provider { get; set; }
        [Required]
        public string Name { get; set; }
        public string AvatarUrl { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public User(ClaimsIdentity userIdentity)
        {
            Token = userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            Provider = userIdentity.FindFirst("Provider").Value;
            Name = userIdentity.FindFirst(ClaimTypes.Name).Value;
            AvatarUrl = userIdentity.FindFirst("Avatar")?.Value;
        }
        public User() { }
    }
}