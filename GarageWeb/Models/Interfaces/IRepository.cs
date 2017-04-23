using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageWeb.Models.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> Data { get; }
        void Add(T entry);
        void Edit(T entry);
        void Remove(int id);
        Task AddAsync(T entry);
        Task EditAsync(T entry);
        Task RemoveAsync(int id);
        void Save();
    }
}
