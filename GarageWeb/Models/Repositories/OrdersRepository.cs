using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GarageWeb.Models;
using GarageWeb.Models.Interfaces;

namespace GarageWeb.Models.Repositories
{
        public class OrdersRepository : IRepository<Order>
        {
            private CoffeDBContext _context = new CoffeDBContext();
            public IQueryable<Order> Data => _context.Orders;

            public void Add(Order entry)
            {
                try
                {
                    entry.Id = DateTime.Now.Month.GetHashCode() + Data.Count();
                    _context.Orders.Add(entry);
                    _context.SaveChanges();
                }
                catch { throw; }
            }

            public Task AddAsync(Order entry)
            {
                return Task.Run(() =>
                {
                    try
                    {
                        entry.Id = DateTime.Now.Month.GetHashCode() + Data.Count();
                        _context.Orders.Add(entry);
                        _context.SaveChanges();
                    }
                    catch { throw; }
                });
            }

            public void Dispose()
            {
                _context.Dispose();
            }

            public void Edit(Order entry)
            {
                try
                {
                    var order = _context.Orders.FirstOrDefault(d => d.Id == entry.Id);
                    order.Status = entry.Status;
                    _context.SaveChanges();
                }
                catch { throw; }
            }

            public Task EditAsync(Order entry)
            {
                return Task.Run(() =>
                {
                    try
                    {
                        var order = _context.Orders.FirstOrDefault(d => d.Id == entry.Id);
                        order.Status = entry.Status;
                        _context.SaveChanges();
                    }
                    catch { throw; }
                });
            }

            public void Remove(int id)
            {
                try
                {
                    _context.Orders.Remove(_context.Orders.First(t => t.Id == id));
                    _context.SaveChanges();
                }
                catch { throw; }
            }

            public Task RemoveAsync(int id)
            {
                return Task.Run(() =>
                {
                    try
                    {
                        _context.Orders.Remove(_context.Orders.First(t => t.Id == id));
                        _context.SaveChanges();
                    }
                    catch { throw; }
                });
            }

            public void Save() => _context.SaveChanges();
        }
}