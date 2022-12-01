using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class Refund_DTO
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; } = Guid.NewGuid().ToString()[..6];
        /// <summary>
        /// 秒杀活动ID
        /// </summary>
        public int SAId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public int Gid { get; set; }
    }
}
