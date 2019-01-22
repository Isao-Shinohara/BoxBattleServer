using BoxBattle.Interfaces;
using MagicOnion;
using MagicOnion.Server;

namespace BoxBattleServer.Hubs
{
	public class GameService : ServiceBase<IGameService>, IGameService
	{
		IRepository repository;

		public GameService()
		{
			repository = ServiceLocator.Get<IRepository>();
		}

		public async UnaryResult<string> CreateAsync(string uuid)
		{
			Logger.Debug($"Received:{uuid}");

			repository.Set("test", uuid);
			var ret = repository.Get<string>("test");

			return ret;
		}
	}
}