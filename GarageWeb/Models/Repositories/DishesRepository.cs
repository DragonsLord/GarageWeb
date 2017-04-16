using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GarageWeb.Models;
using GarageWeb.Models.Interfaces;

namespace GarageWeb.Models.Repositories
{
    public class DishesRepository : IRepository<Dish>
    {
        private CoffeDBContext _context = new CoffeDBContext();
        public IQueryable<Dish> Data => _context.Dishes;

        public async void Add(Dish entry)
        {
            try
            {
                _context.Dishes.Add(entry);
                await _context.SaveChangesAsync();
            } catch { throw; }
        }

        public async void Edit()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch { throw; }
        }

        public async void Remove(Dish entry)
        {
            try
            {
                _context.Dishes.Remove(entry);
                await _context.SaveChangesAsync();
            }
            catch { throw; }
        }
    }
}