using BoxBattleServer.Datas;
using StackExchange.Redis;

namespace BoxBattleServer
{
	public class PlayerRepository : RedisRepository<PlayerData>, IPlayerRepository
	{
		public PlayerRepository(IDatabase db) : base(db)
		{
		}
	}
}
