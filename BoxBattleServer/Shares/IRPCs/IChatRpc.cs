using System.Threading.Tasks;
using MagicOnion;

namespace BoxBattle
{
    public interface IChatRpc : IStreamingHub<IChatRpc, IChatRpcReceiver>
    {
        Task JoinAsync(string userName);
        Task LeaveAsync();
        Task SendMessageAsync(string message);
    }
}
