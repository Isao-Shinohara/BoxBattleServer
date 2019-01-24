using BoxBattleServer.Datas;
using StackExchange.Redis;

namespace BoxBattleServer
{
	public class BattleRepository : RedisRepository<BattleData>, IBattleRepository
	{
		public BattleRepository(IDatabase db) : base(db)
		{
		}
	}
}
