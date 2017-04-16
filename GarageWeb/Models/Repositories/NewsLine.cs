using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GarageWeb.Models.Interfaces;

namespace GarageWeb.Models.Repositories
{
    public class NewsLine : IRepository<NewsEntry>
    {
        private CoffeDBContext _context = new CoffeDBContext();
        public IQueryable<NewsEntry> Data => _context.News;

        public async void Add(NewsEntry entry)
        {
            try
            {
                _context.News.Add(entry);
                await _context.SaveChangesAsync();
            }
            catch { throw; }
        }

        public async void Edit()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch { throw; }
        }

        public async void Remove(NewsEntry entry)
        {
            try
            {
                _context.News.Remove(entry);
                await _context.SaveChangesAsync();
            }
            catch { throw; }
        }
    }
}