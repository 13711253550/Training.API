using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.Seckill
{
    public class seckill_Activity:Base
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
        /// 秒杀库存
        /// </summary>
        public int SeckillNumber { get; set; }
    }
}
