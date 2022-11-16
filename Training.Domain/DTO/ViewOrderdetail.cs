using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Entity;

namespace Training.Domain.DTO
{
    public class ViewOrderdetail
    {
        public DateTime order_time { get; set; }
        public int logistics_company { get; set; }
        public decimal delivery_fee { get; set; }
        public int drug_number { get; set; }
        public string remarks { get; set; }
        public int order_status { get; set; }
        public int UId { get; set; }
        public int DId { get; set; }
    }
}
