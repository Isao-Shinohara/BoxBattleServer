using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BoxBattle
{
    public interface IBattleRpcReceiver
    {
		Task OnJoin(PlayerData playerData);
		Task OnLeave(PlayerData playerData);
		Task OnAttack(PlayerData attackerData, List<PlayerData> defenderDataList);
		Task OnMove(string uuid, Vector3 position, Quaternion rotation, bool moving);
	}
}