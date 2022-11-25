using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class Clinical_Reception_DTO
    {
        public int     Cid     { get; set; }
        public string  UName   { get; set; }
        public string  Cause   { get; set; }
        public string  Time    { get; set; }
        public decimal Price   { get; set; }
        public string  phone   { get; set; }
        public string UserSex  { get; set; }
        public int    UserAge  { get; set; }
    }
}
