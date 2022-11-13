﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Domain.DTO
{
    public class DrugView
    {
        public string Drug_Code { get; set; }
        public string Drug_Name { get; set; }
        public string Drug_Image { get; set; }
        public decimal Drug_Price { get; set; }
        public bool Drug_IsShelves { get; set; }
    }
}
