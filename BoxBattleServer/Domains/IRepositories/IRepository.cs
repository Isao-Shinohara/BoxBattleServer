using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxBattle
{
	public interface IRepository<T> where T : IEntity
	{
		Task<T> GetAsync(string key);
		Task<List<T>> GetListAsync(List<string> keyList);
		Task UpdateAsync(T entity);
		Task UpdateListAsync(List<T> entityList);
	}
}