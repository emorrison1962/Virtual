using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Contracts.Services
{
    public interface IServiceBase<T>
    {
        void Delete(T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(object filter);
        T GetById(int id);
        T GetFullObject(object id);
        IEnumerable<T> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null);
        T Insert(T entity);
        T Update(T entity);
        T Detach(T entity);
    }
}
