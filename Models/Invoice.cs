using FertilizerShopWeb.Models;
using System.ComponentModel.DataAnnotations;

//namespace FertilizerShopWeb.Models
//{
//    public class Invoice
//    {
//    }
//}


//using System.ComponentModel.DataAnnotations;

namespace FertilizerShopWeb.Models
{ 
    public class Invoice
    {

        [Key]
        public int InvoiceId { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        // Customer Info
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }

        // Amount Details
        public decimal SubTotal { get; set; }
        public decimal Vat { get; set; }
        //public decimal TotalAmount { get; set; }   // from 1st model
        public decimal GrandTotal { get; set; }    // from 2nd model



        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();


    }
}
