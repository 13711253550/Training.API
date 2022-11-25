using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class ViewAddPrescription
    {
        public string Drug_Code { get; set; }
        public string Drug_Name { get; set; }
        public string Drug_Image { get; set; }
        public decimal Drug_Price { get; set; }
        public int Drug_Number { get; set; }
        public string inquiry_result_Id { get; set; }
    }
}
