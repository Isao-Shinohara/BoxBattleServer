using StackExchange.Redis;

namespace BoxBattle
{
	public class RedisBattleRepository : RedisRepository<BattleData>, IBattleRepository
	{
		public RedisBattleRepository(IDatabase db) : base(db)
		{
		}
	}
}
