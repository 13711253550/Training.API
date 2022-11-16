using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class Drug_TypeDTO
    {
        /// <summary>
        /// 药品分类名称
        /// </summary>
        public string Drug_Type_Name { get; set; }
        /// <summary>
        /// 药品分类图片
        /// </summary>
        public string Drug_Type_Image { get; set; }
        /// <summary>
        /// 药品分类是否上架
        /// </summary>
        public string Drug_Type_IsShelves { get; set; }
        /// <summary>
        /// 药品类型库存
        /// </summary>
        public int Drug_Type_Stock { get; set; }
    }
}
