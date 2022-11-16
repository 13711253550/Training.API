using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class Orderdetail_DTO
    {
        public int User_Id { get; set; }
        public int Drug_Id { get; set; }
        public DateTime order_time { get; set; }
        public int order_status { get; set; }
        public decimal payable_amount { get; set; }

    }
}
