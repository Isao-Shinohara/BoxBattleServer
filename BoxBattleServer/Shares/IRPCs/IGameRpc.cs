using MagicOnion;

namespace BoxBattle
{
	public interface IGameRpc : IService<IGameRpc>
	{
	  UnaryResult<BattleData> InitializeBattle(string uuid);
	}
}