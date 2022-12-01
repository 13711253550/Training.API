using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.DTO;
using Training.Domain.Entity.UserEntity;
using Training.Domain.Entity.UserEntity.User;
using Training.Domain.Shard;
using Training.EFCore;
using Training.Services.IService;

namespace Training.Services.Service
{
    public class UserService : IUserService
    {
        public IRespotry<User> User;
        public IRespotry<Doctor> Doctor;
        public UserService(IRespotry<User> User, IRespotry<Doctor> Doctor)
        {
            this.User = User;
            this.Doctor = Doctor;
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public Result<List<User>> GetUser()
        {
            return new Result<List<User>>()
            {
                code = stateEnum.Success,
                data = User.GetList().ToList(),
                message = "查询完成"
            };
        }

        //添加医生
        public Result<string> AddDoctor(Doctor doctor)
        {
            Doctor.Add(doctor);
            Doctor.Save();
            return new Result<string>()
            {
                code = stateEnum.Success,
                data = "添加成功",
                message = "添加成功"
            };
        }
    }
}
