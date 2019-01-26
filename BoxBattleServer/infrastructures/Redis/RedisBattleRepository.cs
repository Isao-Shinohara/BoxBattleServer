using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace BoxBattle
{
	public class RedisBattleRepository : RedisRepository<BattleData>, IBattleRepository
	{
		public RedisBattleRepository(IDatabase db) : base(db)
		{
		}

		public new async Task<BattleEntity> GetAsync(string key)
		{
			var data = await base.GetAsync(key);
			if (data == null) return null;
			return new BattleEntity(data);
		}

		public new async Task<List<BattleEntity>> GetListAsync(List<string> keyList)
		{
			var dataList = await base.GetListAsync(keyList);
			return dataList.Select(x => new BattleEntity(x)).ToList();
		}

		public async Task UpdateAsync(string key, BattleEntity entity)
		{
			var data = entity.GenarateData();
			await base.UpdateAsync(key, data);
		}
	}
}
