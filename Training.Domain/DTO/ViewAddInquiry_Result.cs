using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class ViewAddInquiry_Result
    {
        /// <summary>
        /// 接诊记录
        /// </summary>
        public int    Cid                    { get; set; }
        /// <summary>
        /// 主诉
        /// </summary>
        public string action_in_chief        { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string inquiry_result_content { get; set; }
        /// <summary>
        /// 医生开药
        /// </summary>
        public string prescription_Id        { get; set; }
        /// <summary>
        /// 用药方式
        /// </summary>
        public string Drug_method            { get; set; }
    }
}
