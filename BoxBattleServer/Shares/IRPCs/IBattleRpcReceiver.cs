using System.Numerics;
using System.Threading.Tasks;

namespace BoxBattle
{
    public interface IBattleRpcReceiver
    {
		Task OnJoin(string uuid);
		Task OnLeave(string uuid);
		Task OnMove(string uuid, Vector3 position, Quaternion rotation);
	}
}