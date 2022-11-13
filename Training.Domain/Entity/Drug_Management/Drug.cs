using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity.Drug_Management
{
    public class Drug : Base
    {
        /// <summary>
        /// 药品编码
        /// </summary>
        public string Drug_Code { get; set; } = "by" + Guid.NewGuid().ToString("N").Substring(0, 5);
        /// <summary>
        /// 药品名称
        /// </summary>
        public string Drug_Name { get; set; }
        /// <summary>
        /// 药品图片
        /// </summary>
        public string Drug_Image { get; set; }
        /// <summary>
        /// 药品价格
        /// </summary>
        public decimal Drug_Price { get; set; }
        /// <summary>
        /// 药品状态
        /// </summary>
        public bool Drug_IsShelves { get; set; } = true;
        /// <summary>
        /// 药品分类
        /// </summary>
        public int Drug_Type { get; set; }
        /// <summary>
        /// 药品库存
        /// </summary>
        public int Drug_Stock { get; set; }
        /// <summary>
        /// 药品规格
        /// </summary>
        public string Drug_Specification { get; set; }
        /// <summary>
        /// 是否处方药
        /// </summary>
        public bool Drug_IsPrescription { get; set; }
    }
}
