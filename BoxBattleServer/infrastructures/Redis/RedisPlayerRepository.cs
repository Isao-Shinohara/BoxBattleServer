using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace BoxBattle
{
	public class RedisPlayerRepository : RedisRepository<PlayerData>, IPlayerRepository
	{
		public RedisPlayerRepository(IDatabase db) : base(db)
		{
		}

		public new async Task<PlayerEntity> GetAsync(string key)
		{
			var data = await base.GetAsync(key);
			if (data == null) return null;
			return new PlayerEntity(data);
		}

		public new async Task<List<PlayerEntity>> GetListAsync(List<string> keyList)
		{
			var dataList = await base.GetListAsync(keyList);
			return dataList.Select(x => new PlayerEntity(x)).ToList();
		}

		public async Task UpdateAsync(string key, PlayerEntity entity)
		{
			var data = entity.GenarateData();
			await base.UpdateAsync(key, data);
		}
	}
}
