using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//namespace FertilizerShopWeb.Models
//{
//    public class Product
//    {
//    }
//}


//using System.ComponentModel.DataAnnotations;

namespace FertilizerShopWeb.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductType { get; set; } // Seed, Urea, DAP, NPK

        public decimal Price { get; set; }

        public Decimal StockQty { get; set; }


        //public int CompanyId { get; set; }   // FK
        //public Company Company { get; set; }
        [Required(ErrorMessage = "Company is required")]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

    }
}

