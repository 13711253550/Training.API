using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity
{
    public class User:Base
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string account  { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
}
