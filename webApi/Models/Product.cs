using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApi.Models
{
    public class Product
    {
        public string Product_Code { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }

        public List<Tax> Taxes { get; set; }
    }
}