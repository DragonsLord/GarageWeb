using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GarageWeb.Models.Interfaces;

namespace GarageWeb.Models.Repositories
{
    public class NewsLine : IRepository<NewsEntry>
    {
        private CoffeDBContext _context = new CoffeDBContext();
        public IQueryable<NewsEntry> Data => _context.News;

        public void Add(NewsEntry entry)
        {
            try
            {
                _context.News.Add(entry);
                _context.SaveChanges();
            }
            catch { throw; }
        }

        public Task AddAsync(NewsEntry entry)
        {
            return Task.Run(() =>
            {
                try
                {
                    _context.News.Add(entry);
                    _context.SaveChanges();
                }
                catch { throw; }
            });
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Edit(NewsEntry entry)
        {
            try
            {
                var n = _context.News.FirstOrDefault(e => e.Id == entry.Id);
                n.Edit(entry);
                _context.SaveChanges();
            }
            catch { throw; }
        }

        public Task EditAsync(NewsEntry entry)
        {
            return Task.Run(() =>
            {
                try
                {
                    var n = _context.News.FirstOrDefault(e => e.Id == entry.Id);
                    n.Edit(entry);
                    _context.SaveChanges();
                }
                catch { throw; }
            });
        }

        public void Remove(int id)
        {
            try
            {
                _context.News.Remove(_context.News.First(t => t.Id == id));
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
                    _context.News.Remove(_context.News.First(t => t.Id == id));
                    _context.SaveChanges();
                }
                catch { throw; }
            });
        }
        public void Save() => _context.SaveChanges();
    }
}