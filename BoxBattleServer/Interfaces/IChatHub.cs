using System.Threading.Tasks;
using MagicOnion;

namespace BoxBattle.Interfaces
{
    public interface IChatHub : IStreamingHub<IChatHub, IChatHubReceiver>
    {
        Task JoinAsync(string userName);
        Task LeaveAsync();
        Task SendMessageAsync(string message);
    }
}
