using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.JWT
{
    public class JWT : Base
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 验证JWT
        /// </summary>
        public string verification_JWT { get; set; }
        /// <summary>
        /// 刷新JWT
        /// </summary>
        public string renovation_JWT { get; set; }
    }
}
