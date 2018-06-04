using System.Collections.Generic;
using System.Linq;

namespace Recipes.Contracts.Repositories
{
	public interface IRepositoryBase<T>
	{
		void Commit();
		void Delete(T entity);
		void Delete(int id);
		void Dispose();
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(object filter);
		T GetById(int id);
		T GetFullObject(object id);
        IEnumerable<T> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null);
		void Insert(T entity);
		void Update(T entity);
        T Detach(T entity);
	}
}