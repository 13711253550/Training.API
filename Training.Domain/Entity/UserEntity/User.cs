using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.UserEntity.User
{
    public class User : Base
    { 
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string account { get; set; }
        [Required]
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string? address { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string? phone { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public int UserSex { get; set; }
        /// <summary>
        /// 用户年龄
        /// </summary>
        public int UserAge { get; set; }
    }
}
