using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageWeb.Models.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Data { get; }
        void Add(T entry);
        void Edit();
        void Remove(int id);
    }
}
