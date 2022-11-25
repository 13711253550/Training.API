using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class UserShow_DTO
    {
        public int      Id          { get; set; }
        public string   account     { get; set; }
        public string?  phone       { get; set; }
        public int      UserSex     { get; set; }
        public string   RoleName    { get; set; }
    }
}
