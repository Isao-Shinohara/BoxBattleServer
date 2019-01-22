using MessagePack;
using StackExchange.Redis;

namespace BoxBattleServer
{
	public class RedisRepository : IRepository
	{
		private IDatabase db;

		public RedisRepository(IDatabase db)
		{
			this.db = db;
		}

		public T Get<T>(string key)
		{
			var value = db.StringGet(key);
			return MessagePackSerializer.Deserialize<T>(value);
		}

		public void Set<T>(string key, T value)
		{
			var bytes = MessagePackSerializer.Serialize(value);
			db.StringSet(key, bytes);
		}
	}
}