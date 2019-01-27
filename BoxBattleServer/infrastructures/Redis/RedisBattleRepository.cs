using StackExchange.Redis;

namespace BoxBattle
{
	public class RedisBattleRepository : RedisRepository<BattleEntity>, IBattleRepository
	{
		public RedisBattleRepository(IDatabase db) : base(db)
		{
		}
	}
}
