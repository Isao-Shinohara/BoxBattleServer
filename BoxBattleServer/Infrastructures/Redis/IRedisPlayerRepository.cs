using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxBattle
{
	public interface IRedisPlayerRepository
	{
		Task<PlayerEntity> GetAsync(string key);
		Task<List<PlayerEntity>> GetListAsync(List<string> keyList);
		Task UpdateAsync(PlayerEntity entity);
	}
}