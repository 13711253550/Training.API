using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class DrugOrder_DTO
    {
        /// <summary>
        /// 药品Id
        /// </summary>
        public string Drug_Id { get; set; }
        /// <summary>
        /// 购买药品数量
        /// </summary>
        public int Drug_Number { get; set; }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string drug_name { get; set; }
        /// <summary>
        /// 药品图片
        /// </summary>
        public string drug_img { get; set; }
        /// <summary>
        /// 药品价格
        /// </summary>
        public decimal drug_price { get; set; }
        /// <summary>
        /// 诊断结果
        /// </summary>
        public string inquiry_result_Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int User_Id { get; set; }
        /// <summary>
        /// 就诊记录Id
        /// </summary>
        public int Cid { get; set; }
    }
}
