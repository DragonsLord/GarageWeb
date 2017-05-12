using System.Collections.Generic;
using System.Linq;
using GarageWeb.Models.ViewModel;
using GarageWeb.Models.Interfaces;
using System;

namespace GarageWeb.Models
{
    public class Basket
    {
        private List<DishOrderViewModel> _orderDishes;

        public List<DishOrderViewModel> Orders  => _orderDishes;
        public int Count => _orderDishes.Count;
        public double Price => _orderDishes.Sum(t => t.Count*t.Dish.Price);

        public Basket()
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
            else d.Count++;
        }
        public void RemoveDish(Dish dish)
        {
            _orderDishes.RemoveAll(t => t.Dish.Id ==dish.Id);
        }

        internal void RemoveOnePortion(Dish dish)
        {
            DishOrderViewModel d = _orderDishes.FirstOrDefault(t => t.Dish.Id == dish.Id);
            if (d != null)
            {
                if (d.Count==1) RemoveDish(dish);
                else d.Count--;
            }
        }

        public void Clear()
        {
            _orderDishes.Clear();
        }
    }
}