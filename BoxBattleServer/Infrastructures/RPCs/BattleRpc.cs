using System.Numerics;
using System.Threading.Tasks;
using MagicOnion.Server.Hubs;

namespace BoxBattle
{
	public class BattleRpc : StreamingHubBase<IBattleRpc, IBattleRpcReceiver>, IBattleRpc
	{
		IGroup room;

		public async Task JoinAsync(string uuid)
		{
			room = await Group.AddAsync("BattleRoom");
			Broadcast(room).OnJoin(uuid);
		}

		public async Task LeaveAsync(string uuid)
		{
			await room.RemoveAsync(Context);
			Broadcast(room).OnLeave(uuid);
		}

		public async Task Move(string uuid, Vector3 position, Quaternion rotation)
		{
			Broadcast(room).OnMove(uuid, position, rotation);
		}
	}
}