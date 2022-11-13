using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.EFCore;

namespace Training.Domain.Entity.Drug_Management
{
    /// <summary>
    /// 药品分类表
    /// </summary>
    public class Drug_Type : Base
    {
        /// <summary>
        /// 药品分类名称
        /// </summary>
        public int Drug_Type_Name { get; set; }
        /// <summary>
        /// 药品分类图片
        /// </summary>
        public string Drug_Type_Image { get; set; }
        /// <summary>
        /// 药品分类是否上架
        /// </summary>
        public bool Drug_Type_IsShelves { get; set; }
        /// <summary>
        /// 药品类型库存
        /// </summary>
        public int Drug_Type_Stock { get; set; }
    }

}
