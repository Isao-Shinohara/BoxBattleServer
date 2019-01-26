using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagePack;
using StackExchange.Redis;

namespace BoxBattle
{
	public class RedisRepository<T> : IRepository<T> where T : BaseData
	{
		protected IDatabase db;

		public RedisRepository(IDatabase db)
		{
			this.db = db;
		}

		public async Task<T> GetAsync(string key)
		{
			var bytes = await db.StringGetAsync(key);
			if (bytes.IsNull) return default(T);
			return MessagePackSerializer.Deserialize<T>(bytes);
		}

		public async Task<List<T>> GetListAsync(List<string> keyList)
		{
			var list = new List<T>();
			await Task.WhenAll(keyList.Select(async x => {
				var data = await GetAsync(x);
				list.Add(data);
			}));
			return list;
		}

		public async Task UpdateAsync(string key, T data)
		{
			var bytes = MessagePackSerializer.Serialize(data);
			await db.StringSetAsync(key, bytes);
		}
	}
}