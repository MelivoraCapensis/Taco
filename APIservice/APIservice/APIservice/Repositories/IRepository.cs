using System.Collections.Generic;
using APIservice.Models;

namespace APIservice.Repositories
{
    public interface IRepository<T> where T : class, IEntity
        {
           List<T> GetAllRecords();
            T GetItem(int id);
            void Add(T entity);
            void Update(T entity);
            void Delete(int id);
        }
}