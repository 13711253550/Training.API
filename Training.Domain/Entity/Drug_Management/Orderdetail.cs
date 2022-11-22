using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Entity.UserEntity.User;

namespace Training.Domain.Entity.Drug_Management
{
    /// <summary>
    /// 订单详情表
    /// </summary>
    public class Orderdetail : Base
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string payment_number { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 16);
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime order_time { get; set; } = DateTime.Now;
        /// <summary>
        /// 支付编码,自动生成随机不重复16位支付编码
        /// </summary>
        public string payment_code { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 16);
        /// <summary>
        /// 物流公司
        /// </summary>1
        public int logistics_company { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public string? logistics_number { get; set; } = "";
        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal delivery_fee { get; set; }
        /// <summary>
        /// 药品数量
        /// </summary>
        public int drug_number { get; set; }
        /// <summary>
        /// 发货单备注
        /// </summary>
        public string remarks { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int order_status { get; set; }
        public User User { get; set; }
        public Drug Drug { get; set; }
    }
}
