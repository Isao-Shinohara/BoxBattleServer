using System.Threading.Tasks;
using MagicOnion.Server.Hubs;
using UnityEngine;

namespace BoxBattle
{
	public class BattleRpc : StreamingHubBase<IBattleRpc, IBattleRpcReceiver>, IBattleRpc
	{
		IGroup room;
		BattleService battleService;

		public BattleRpc()
		{
			battleService = ServiceLocator.Get<BattleService>();
		}

		public async Task JoinAsync(string uuid)
		{
			room = await Group.AddAsync("BattleRoom");
			var playerEntity = await battleService.JoinAsync(uuid);
			Broadcast(room).OnJoin(playerEntity.GenarateData());
		}

		public async Task LeaveAsync(string uuid)
		{
			await room.RemoveAsync(Context);
			var playerEntity = await battleService.LeaveAsync(uuid);
			Broadcast(room).OnLeave(playerEntity.GenarateData());
		}

		public async Task Move(string uuid, Vector3 position, Quaternion rotation, bool moving)
		{
			Broadcast(room).OnMove(uuid, position, rotation, moving);
		}
	}
}