using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.UserEntity
{
    /// <summary>
    /// 医生信息
    /// </summary>
    public class Doctor : Base
    {
        private string Password;

        /// <summary>
        /// 医生名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string account { get; set; }
        [Required]
        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            get
            {
                return Password;
            }
            set
            {
                Password = MD5Hash.Hash.Content(value);
            }
        }
    }
}
