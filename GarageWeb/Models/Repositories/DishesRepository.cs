using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GarageWeb.Models;
using GarageWeb.Models.Interfaces;

namespace GarageWeb.Models.Repositories
{
    public class DishesRepository : IRepository<Dish>
    {
        private CoffeDBContext _context = new CoffeDBContext();
        public IQueryable<Dish> Data => _context.Dishes;

        public void Add(Dish entry)
        {
            try
            {
                _context.Dishes.Add(entry);
                _context.SaveChangesAsync();
            } catch { throw; }
        }

        public Task AddAsync(Dish entry)
        {
            return Task.Run(() =>
            {
                try
                {
                    _context.Dishes.Add(entry);
                    _context.SaveChanges();
                }catch { throw; }
            });
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Edit(Dish entry)
        {
            try
            {
                var dish = _context.Dishes.FirstOrDefault(d => d.Id == entry.Id);
                dish.Edit(entry);
                _context.SaveChangesAsync();
            }
            catch { throw; }
        }

        public Task EditAsync(Dish entry)
        {
            return Task.Run(() =>
            {
                try
                {
                    var dish = _context.Dishes.FirstOrDefault(d => d.Id == entry.Id);
                    dish.Edit(entry);
                    _context.SaveChanges();
                }
                catch { throw; }
            });
        }

        public void Remove(int id)
        {
            try
            {
                _context.Dishes.Remove(_context.Dishes.First(t=>t.Id==id));
                _context.SaveChangesAsync();
            }
            catch { throw; }
        }

        public Task RemoveAsync(int id)
        {
            return Task.Run(() =>
            {
                try
                {
                    _context.Dishes.Remove(_context.Dishes.First(t => t.Id == id));
                    _context.SaveChanges();
                }
                catch { throw; }
            });
        }

        public void Save() => _context.SaveChanges();
    }
}