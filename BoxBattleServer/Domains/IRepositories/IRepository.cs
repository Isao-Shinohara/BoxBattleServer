using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxBattle
{
	public interface IRepository<T>
	{
		Task<T> GetAsync(string key);
		Task<List<T>> GetListAsync(List<string> keyList);
		Task UpdateAsync(string key, T entity);
	}
}