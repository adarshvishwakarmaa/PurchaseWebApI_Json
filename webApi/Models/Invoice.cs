using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApi.Models
{
    public class Invoice
    {
        public string Invoice_No { get; set; }
        public DateTime Invoice_Date { get; set; }
        public string Supplier_Code { get; set; }

        public List<Product> Products { get; set; }
    }
}