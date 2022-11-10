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
    public class Result
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public stateEnum    Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string       Message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object       Data { get; set; }
    }
}
