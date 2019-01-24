using System.Collections.Generic;
using BoxBattleServer.Datas;
using MagicOnion;

namespace BoxBattle.Interfaces
{
	public interface IGameService : IService<IGameService>
	{
	  UnaryResult<List<PlayerData>> JoinBattle(string uuid);
	}
}