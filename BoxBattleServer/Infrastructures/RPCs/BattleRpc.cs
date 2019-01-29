using System.Collections.Generic;
using System.Linq;
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
			var playerEntity = await battleService.LeaveAsync(uuid);
			await BroadcastExceptSelf(room).OnLeave(playerEntity.GenarateData());
			await room.RemoveAsync(Context);
		}

		public async Task Attack(string attackerUuid, int attackerMp, List<string> defenderUuidList)
		{
			var (attacker, defenderList) = await battleService.Attack(attackerUuid, attackerMp, defenderUuidList);
			var defenderDataList = defenderList.Select(p => p.GenarateData()).ToList();
			Broadcast(room).OnAttack(attacker.GenarateData(), defenderDataList);
		}

		public async Task Recover(string uuid)
		{
			var player = await battleService.Recover(uuid);
			Broadcast(room).OnRecover(player.GenarateData());
		}

		public async Task ChargeMpStart(string uuid)
		{
			Broadcast(room).OnChargeMpStart(uuid);
		}

		public async Task ChargeMpStop(string uuid)
		{
			Broadcast(room).OnChargeMpStop(uuid);
		}

		public async Task Move(string uuid, Vector3 position, Quaternion rotation, bool moving)
		{
			battleService.Move(uuid, position, rotation);
			Broadcast(room).OnMove(uuid, position, rotation, moving);
		}
	}
}