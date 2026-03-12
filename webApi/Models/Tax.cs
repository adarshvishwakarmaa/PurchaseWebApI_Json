using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApi.Models
{
    public class Tax
    {
        public string Tax_Type { get; set; }
        public decimal Tax_Percent { get; set; }
    }
}