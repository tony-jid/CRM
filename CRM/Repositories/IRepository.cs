using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IRepository<T> 
        where T : class
    {
        IEnumerable<T> Get();

        T Get(int id);

        T GetByUid(Guid uid);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
