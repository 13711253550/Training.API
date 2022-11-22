using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.Entity
{
    public class Message
    {
        public string   Id      { get; set; }
        public string   name    { get; set; }
        public string   url     { get; set; }
        public string   content { get; set; }
        public bool     show    { get; set; }
        public DateTime time    { get; set; }
    }
}
