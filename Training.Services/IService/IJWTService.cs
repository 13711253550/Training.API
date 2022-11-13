using Training.Domain.Shard;

namespace Training.Services.IService
{
    public interface IJWTService
    {
        string GetToken(string token);
        int GetUserName(string Token);
        Result<string> Login(string account, string password);
    }
}