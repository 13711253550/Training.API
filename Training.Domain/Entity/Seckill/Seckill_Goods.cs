using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.Seckill
{
    /// <summary>
    /// 秒杀活动表
    /// </summary>
    public class Seckill_Goods:Base
    {
        /// <summary>
        /// 秒杀活动Id
        /// </summary>
        public int SeckillId { get; set; }
        /// <summary>
        /// 秒杀商品Id
        /// </summary>
        public int GoodsId   { get; set; }
    }
}
