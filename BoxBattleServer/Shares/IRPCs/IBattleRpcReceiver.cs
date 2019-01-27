using System.Threading.Tasks;
using UnityEngine;

namespace BoxBattle
{
    public interface IBattleRpcReceiver
    {
		Task OnJoin(string uuid);
		Task OnLeave(string uuid);
		Task OnMove(string uuid, Vector3 position, Quaternion rotation, bool moving);
	}
}