using Training.Domain.Entity;

namespace Training.Services.Service
{
    public interface IJWTService
    {
        int GetUserName(string Token);
        object Login(string account, string password);
        string GetToken(string token);
    }
}