using MagicOnion;

namespace BoxBattle.Interfaces
{
	public interface IGameService : IService<IGameService>
	{
	  UnaryResult<string> CreateAsync(string uuid);
	}
}