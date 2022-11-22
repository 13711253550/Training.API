using Training.Domain.Entity;
using Training.Domain.Shard;

namespace Training.Services.IService
{
    public interface IUserService
    {
        Result<List<User>> GetUser();
    }
}