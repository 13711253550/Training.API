using Training.Domain.Entity.UserEntity.User;
using Training.Domain.Shard;

namespace Training.Services.IService
{
    public interface IUserService
    {
        Result<List<User>> GetUser();
    }
}