using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Shard
{
    /// <summary>
    /// 状态码枚举
    /// </summary>
    public enum stateEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 200,
        /// <summary>
        /// 失败
        /// </summary>
        Error = 500,
    }
}
