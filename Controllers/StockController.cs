using FertilizerShopWeb.Data;
using FertilizerShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//namespace FertilizerShopWeb.Controllers
//{
//    public class StockController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}

//using Fertilizer_And_Seed_management_System.Data;
//using Fertilizer_And_Seed_management_System.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace FertilizerShopWeb.Controllers
{
    public class StockController : Controller
    {

        private readonly AppDbContext _context;

        public StockController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            // Stock List
            var data = _context.Stocks
            .Include(s => s.Product)
            .OrderByDescending(s => s.EntryDate)
            .ToList();

            return View(data);
        }


        // GET: Stock In
        public IActionResult StockIn()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // POST: Stock In
        [HttpPost]
        public IActionResult StockIn(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);

            if (product == null) return NotFound();

            // Stock table entry
            Stock stock = new Stock
            {
                ProductId = productId,
                Quantity = quantity,
                StockType = "IN"
            };

            // Product stock update
            product.StockQty += quantity;

            _context.Stocks.Add(stock);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Stock Out
        public IActionResult StockOut()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // POST: Stock Out
        [HttpPost]
        public IActionResult StockOut(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);

            if (product.StockQty < quantity)
            {
                ViewBag.Error = "Not enough stock!";
                ViewBag.Products = _context.Products.ToList();
                return View();
            }
            Stock stock = new Stock
            {
                ProductId = productId,
                Quantity = quantity,
                StockType = "OUT"
            };

            product.StockQty -= quantity;

            _context.Stocks.Add(stock);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }
    }
}

