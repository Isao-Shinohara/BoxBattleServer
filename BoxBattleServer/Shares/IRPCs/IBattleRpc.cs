using System.Numerics;
using System.Threading.Tasks;
using MagicOnion;

namespace BoxBattle
{
	public interface IBattleRpc : IStreamingHub<IBattleRpc, IBattleRpcReceiver>
	{
		Task JoinAsync(string uuid);
		Task LeaveAsync(string uuid);
		Task Move(string uuid, Vector3 position, Quaternion rotation);
	}
}