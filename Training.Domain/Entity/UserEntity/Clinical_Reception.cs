using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.UserEntity
{
    /// <summary>
    /// 接诊信息
    /// </summary>
    public class Clinical_Reception : Base
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 医生id
        /// </summary>
        public int Did { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UName { get; set; }
        /// <summary>
        /// 患者症状
        /// </summary>
        public string Cause { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string Time { get; set; } = DateTime.Now.ToString("g");
        /// <summary>
        /// 就诊金额
        /// </summary>
        public decimal Price { get; set; } = 0;
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
        /// <summary>
        /// 是否解决
        /// </summary>
        public bool state { get; set; } = false;
    }
}
