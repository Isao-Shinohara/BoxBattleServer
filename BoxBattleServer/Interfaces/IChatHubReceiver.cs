namespace BoxBattle.Interfaces
{
    public interface IChatHubReceiver
    {
        void OnJoin(string name);
        void OnLeave(string name);
        void OnSendMessage(string name, string message);
    }
}