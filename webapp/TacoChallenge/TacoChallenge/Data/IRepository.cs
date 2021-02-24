using System.Collections.Generic;
using System.Threading.Tasks;

namespace TacoChallenge.Data
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