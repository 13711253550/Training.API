using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.UserEntity
{
    public class Label:Base
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string LabelName { get; set; }
        /// <summary>
        /// 标签描述
        /// </summary>
        public string introduce { get; set; }
    }
}
