using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Shard
{
    /// <summary>
    /// 统一返回结果
    /// </summary>
    public class Result<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public stateEnum    code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string       message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T       data { get; set; }
    }
}
