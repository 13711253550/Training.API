namespace Training.Services.IService
{
    public interface ISignaIRChatService
    {
        Task SendMessage(string user, string message);
        Task SendMessageToCaller(string user, string message);
    }
}