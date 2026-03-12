using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using webApi.Models;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace webApi.Controllers
{
    public class PurchaseInvoiceController : ApiController
    {
        string conn = @"Data Source=KRISH\SQL2025;Initial Catalog=PurchaseDb;Integrated security=True";

        [HttpPost]

        [Route("api/PurchaseInvoice")]
        public HttpResponseMessage SaveInvoice([FromBody] SaveVM model)
        {
            if (model == null || model.CompInv_data == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");
            }
            using (SqlConnection cn = new SqlConnection(conn)) 
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction();

                try
                {
                    foreach(var invoice in model.CompInv_data)
                    {
                        SqlCommand cmd = new SqlCommand(@"
                                INSERT INTO Invoice
                                (InvoiceNo,InvoiceDate,SupplierCode)

                                OUTPUT INSERTED.ID

                                VALUES
                                (@InvoiceNo,@InvoiceDate,@SupplierCode)
                                ", cn, tr);
                        cmd.Parameters.AddWithValue("@InvoiceNo", invoice.Invoice_No);
                        cmd.Parameters.AddWithValue("@InvoiceDate", invoice.Invoice_Date);
                        cmd.Parameters.AddWithValue("@SupplierCode", invoice.Supplier_Code);

                        int invoiceId = Convert.ToInt32(cmd.ExecuteScalar());

                        foreach(var product in invoice.Products)
                        {
                            SqlCommand cmd2 = new SqlCommand(@"
                                    INSERT INTO Product
                                    (InvoiceId,ProductCode,Qty,Rate)
                                    
                                    OUTPUT INSERTED.ID

                                    VALUES
                                    (@InvoiceId,@ProductCode,@Qty,@Rate)
                                    ", cn, tr);

                            cmd2.Parameters.AddWithValue("@InvoiceId", invoiceId);
                            cmd2.Parameters.AddWithValue("@ProductCode", product.Product_Code);
                            cmd2.Parameters.AddWithValue("@Qty", product.Qty);
                            cmd2.Parameters.AddWithValue("@Rate", product.Rate);

                            int productId = Convert.ToInt32(cmd2.ExecuteScalar());

                            foreach (var tax in product.Taxes)
                            {

                                SqlCommand cmd3 = new SqlCommand(@"
                                INSERT INTO ProductTax
                                (ProductId,TaxType,TaxPercent)

                                VALUES
                                (@ProductId,@TaxType,@TaxPercent)
                                ", cn, tr);

                                cmd3.Parameters.AddWithValue("@ProductId", productId);
                                cmd3.Parameters.AddWithValue("@TaxType", tax.Tax_Type);
                                cmd3.Parameters.AddWithValue("@TaxPercent", tax.Tax_Percent);

                                cmd3.ExecuteNonQuery();
                            }
                        }

                    }
                    tr.Commit();

                    return Request.CreateResponse(HttpStatusCode.OK, "Invoice Saved");

                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            
        }


    }
}