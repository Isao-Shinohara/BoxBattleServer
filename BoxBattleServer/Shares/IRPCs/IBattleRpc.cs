using System.Collections.Generic;
using System.Threading.Tasks;
using MagicOnion;
using UnityEngine;

namespace BoxBattle
{
	public interface IBattleRpc : IStreamingHub<IBattleRpc, IBattleRpcReceiver>
	{
		Task JoinAsync(string uuid, Vector3 position, Quaternion rotation);
		Task LeaveAsync(string uuid);
		Task Attack(string attackerUuid, int attackerMp, List<string> defenderUuidList);
		Task ChargeMpStart(string uuid);
		Task ChargeMpStop(string uuid);
		Task Recover(string uuid);
		Task CharacterMoving(string uuid, Vector3 position, Quaternion rotation, bool moving);
		Task Move(string uuid, Vector3 position, Quaternion rotation);
	}
}