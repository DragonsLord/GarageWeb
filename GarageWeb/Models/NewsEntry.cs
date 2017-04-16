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
        public string Titile { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[] Image { get; set; }
    }
}