using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.Seckill
{
    public class SeckillOrder : Base
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
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState      { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime   { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal? PayMoney   { get; set; }
    }
}
