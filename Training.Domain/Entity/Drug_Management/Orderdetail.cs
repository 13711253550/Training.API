using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.Drug_Management
{
    /// <summary>
    /// 订单详情表
    /// </summary>
    public class Orderdetail : Base
    {

        /// <summary>
        /// 自动生成随机不重复16位订单号
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
        /// 收货人
        /// </summary>
        public string consignee { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string consignee_phone { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string consignee_address { get; set; }
        /// <summary>
        /// 物流公司
        /// </summary>
        public int logistics_company { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public string logistics_number { get; set; }
        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal delivery_fee { get; set; }
        /// <summary>
        /// 药品图片
        /// </summary>
        public string drug_image { get; set; }
        /// <summary>
        /// 药品介绍
        /// </summary>
        public string drug_introduction { get; set; }
        /// <summary>
        /// 药品数量
        /// </summary>
        public int drug_number { get; set; }
        /// <summary>
        /// 药品规格
        /// </summary>
        public string drug_specification { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal payable_amount { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal actual_amount { get; set; }
        /// <summary>
        /// 发货单备注
        /// </summary>
        public string remarks { get; set; }
    }
}
