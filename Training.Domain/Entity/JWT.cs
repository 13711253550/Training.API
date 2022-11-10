using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity
{
    public class JWT : Base
    {
        public int    Uid               { get; set; }
        public string verification_JWT  { get; set; }
        public string renovation_JWT    { get; set; }
    }
}
