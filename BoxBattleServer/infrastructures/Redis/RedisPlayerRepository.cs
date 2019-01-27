using StackExchange.Redis;

namespace BoxBattle
{
	public class RedisPlayerRepository : RedisRepository<PlayerEntity>, IPlayerRepository
	{
		public RedisPlayerRepository(IDatabase db) : base(db)
		{
		}
	}
}
