using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxBattleServer
{
	public interface IRepository<T>
	{
		Task<T> GetAsync(string key);
		Task<List<T>> GetListAsync(List<string> keyList);
		Task UpdateAsync(string key, T value);
	}
}