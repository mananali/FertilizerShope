//using FertilizerShopWeb.Models;
//using System.ComponentModel.DataAnnotations;

////namespace FertilizerShopWeb.Models
////{
////    public class InvoiceItem
////    {
////    }
////}


////using System.ComponentModel.DataAnnotations;

//namespace FertilizerShopWeb.Models
//{ 
//    public class InvoiceItem
//    {

//        [Key]
//        public int InvoiceItemId { get; set; }

//        public int InvoiceId { get; set; }
//        public int ProductId { get; set; }

//        public string ProductName { get; set; }

//        public decimal Qty { get; set; }      // Quantity
//        public decimal Rate { get; set; }     // Price per unit

//        // Calculated Amount
//        public decimal Amount => Qty * Rate;


//        // Navigation
//        public Product Product { get; set; }
//        public Invoice Invoice { get; set; }

//    }
//}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FertilizerShopWeb.Models
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }

        public int InvoiceId { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Qty { get; set; }
        public decimal Rate { get; set; }

        [NotMapped]
        public decimal Amount => Qty * Rate;

        public Product Product { get; set; }
        public Invoice Invoice { get; set; }
    }
}

