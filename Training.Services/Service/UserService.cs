using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Entity;
using Training.Domain.Shard;
using Training.EFCore;
using Training.Services.IService;

namespace Training.Services.Service
{
    public class UserService : IUserService
    {
        public IRespotry<User> User;
        public UserService(IRespotry<User> User)
        {
            this.User = User;
        }

        public Result<List<User>> GetUser()
        {
            return new Result<List<User>>()
            {
                code = stateEnum.Success,
                data = User.GetList().ToList(),
                message = "查询完成"
            };
        }
    }
}
