using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.UserEntity
{
    /// <summary>
    /// 用户角色信息
    /// </summary>
    public class UserRole : Base
    {
        public int Uid { get; set; }
        public int Rid { get; set; }
    }
}
