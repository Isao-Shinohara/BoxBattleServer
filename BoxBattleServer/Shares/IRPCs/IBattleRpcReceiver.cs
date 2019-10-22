using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BoxBattle
{
    public interface IBattleRpcReceiver
    {
		void OnJoin(PlayerData playerData);
		void OnLeave(PlayerData playerData);
		void OnAttack(PlayerData attackerData, List<PlayerData> defenderDataList);
		void OnChargeMpStart(string uuid);
		void OnChargeMpStop(string uuid);
		void OnRecover(PlayerData playerData);
		void OnCharacterMoving(string uuid, Vector3 position, Quaternion rotation, bool moving);
		void OnMove(string uuid, Vector3 position, Quaternion rotation);
	}
}