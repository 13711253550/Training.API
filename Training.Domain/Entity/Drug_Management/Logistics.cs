using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.Drug_Management
{
    /// <summary>
    /// 物流公司表
    /// </summary>
    public class Logistics : Base
    {
        /// <summary>
        /// 物流公司名称		
        /// </summary>
        public string logistics_company_name { get; set; }
    }
}
