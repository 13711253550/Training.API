using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class SeckillOrder_DTO
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; } = Guid.NewGuid().ToString()[..6];
        /// <summary>
        /// 秒杀活动ID
        /// </summary>
        public string SName { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UName { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public string GName { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 支付时间
        /// </summary>
        public string? PayTime { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal? PayMoney { get; set; }
        public int SAId { get; set; }
        public int Uid { get; set; }
        public int Gid { get; set; }
    }
}
