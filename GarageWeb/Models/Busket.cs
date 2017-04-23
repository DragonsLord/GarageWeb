using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GarageWeb.Models.ViewModel;

namespace GarageWeb.Models
{
    public class Busket
    {
        private List<DishOrderViewModel> _orderDishes;
        public List<DishOrderViewModel> Orders  => _orderDishes;
        public Busket()
        {
            _orderDishes = new List<DishOrderViewModel>();
        }
        public void AddDish(Dish dish)
        {
            DishOrderViewModel d = _orderDishes.FirstOrDefault(t => t.Dish.Id == dish.Id);
            if (d==null)
            {
                _orderDishes.Add(new DishOrderViewModel
                {
                    Dish = dish,
                    Count = 1
                });
            }
        }
        public void RemoveDish(Dish dish)
        {
            _orderDishes.RemoveAll(t => t.Dish == dish);
        }
        public void Clear()
        {
            _orderDishes.Clear();
        }
        public int Count => _orderDishes.Count;
        public double TotalWorth => _orderDishes.Sum(t => t.Dish.Price);

    }
}