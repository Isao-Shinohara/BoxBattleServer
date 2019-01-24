namespace BoxBattle
{
    public interface IChatRpcReceiver
    {
        void OnJoin(string name);
        void OnLeave(string name);
        void OnSendMessage(string name, string message);
    }
}