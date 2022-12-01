using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class Seckill_Show_DTO
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivityStatus { get; set; }
        /// <summary>
        /// 秒杀价格
        /// </summary>
        public decimal SeckillPrice { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品图片　
        /// </summary>
        public string GoodsImg { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public int GoodsNumber { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GoodsPrice { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public string GoodsId { get; set; }
        /// <summary>
        /// 活动Id
        /// </summary>
        public string SeckillId { get; set; }
        /// <summary>
        /// 秒杀库存
        /// </summary>
        public string SeckillNumber { get; set; }
        public string ActivityStatusName { get; set; }

    }
}
