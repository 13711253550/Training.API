using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.Seckill
{
    public class goods:Base
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string  GoodsName { get; set; }
        /// <summary>
        /// 商品图片　
        /// </summary>
        public string  GoodsImg { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public int     GoodsNumber { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GoodsPrice { get; set; }
    }
}
