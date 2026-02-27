using FertilizerShopWeb.Models;
using System.ComponentModel.DataAnnotations;

//namespace FertilizerShopWeb.Models
//{
//    public class Stock
//    {
//    }
//}


//using System.ComponentModel.DataAnnotations;

namespace FertilizerShopWeb.Models
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string StockType { get; set; } // IN / OUT

        public DateTime EntryDate { get; set; } = DateTime.Now;

        // Relation
        public Product Product { get; set; }
    }
}
