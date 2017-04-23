using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageWeb.Models.ViewModel
{
    public class DishOrderViewModel
    {
        public Dish Dish{get;set;}
        public int Count { get; set; }
    }
}