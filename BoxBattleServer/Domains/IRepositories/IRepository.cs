using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxBattle
{
	public interface IRepository<T, U> where U : IEntity<T>
	{
		Task Save();
		Task<U> GetAsync(string key);
		Task<List<U>> GetListAsync(List<T> keyList);
		Task DeleteAsync(string key);
		Task UpdateAsync(U entity);
		Task UpdateListAsync(List<U> entityList);
	}
}