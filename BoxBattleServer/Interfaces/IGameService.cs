using BoxBattleServer.Datas;
using MagicOnion;

namespace BoxBattle.Interfaces
{
	public interface IGameService : IService<IGameService>
	{
	  UnaryResult<BattleData> JoinBattle(string uuid);
	}
}