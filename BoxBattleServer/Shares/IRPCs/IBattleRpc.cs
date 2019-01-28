using System.Collections.Generic;
using System.Threading.Tasks;
using MagicOnion;
using UnityEngine;

namespace BoxBattle
{
	public interface IBattleRpc : IStreamingHub<IBattleRpc, IBattleRpcReceiver>
	{
		Task JoinAsync(string uuid);
		Task LeaveAsync(string uuid);
		Task Attack(string attackerUuid, int attackerMp, List<string> defenderUuidList);
		Task Move(string uuid, Vector3 position, Quaternion rotation, bool moving);
	}
}