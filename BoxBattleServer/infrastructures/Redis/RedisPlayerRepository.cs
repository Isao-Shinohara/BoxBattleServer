using StackExchange.Redis;

namespace BoxBattle
{
	public class RedisPlayerRepository : RedisRepository<PlayerData>, IPlayerRepository
	{
		public RedisPlayerRepository(IDatabase db) : base(db)
		{
		}
	}
}
