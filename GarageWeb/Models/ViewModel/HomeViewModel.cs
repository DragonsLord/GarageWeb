using System.Collections.Generic;

namespace GarageWeb.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Dish> Dishes { get; set; }
        public IEnumerable<NewsEntry> News { get; set; }
    }
}