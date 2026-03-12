using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApi.Models
{
    public class SaveVM
    {
        public string UserID { get; set; }

        public List<Invoice> CompInv_data { get; set; }
        public List<Invoice> SalesInvoice_data { get; set; }
        public List<Invoice> SalesReturn_data { get; set; }
    }
}