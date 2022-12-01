using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class ViewAddAlipayTrade
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 购买商品名称
        /// </summary>
        public string Subject { get; set; }
    }
}
