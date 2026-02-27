using FertilizerShopWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace FertilizerShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Session check
            //if (HttpContext.Session.GetString("UserId") == null)
            //    return RedirectToAction("Login", "User");

            //// Dashboard counts
            //ViewBag.ProductCount = _context.Products.Count();
            //ViewBag.CompanyCount = _context.Companies.Count(c => c.IsActive);
            //ViewBag.InvoiceCount = _context.Invoices.Count();
            //ViewBag.UserCount = _context.Users.Count();

            //// ✅ Role for navbar
            ////ViewBag.UserRole = HttpContext.Session.GetString("UserRole");

            //return View();

            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("UserRole");

            ViewBag.UserId = userId;
            ViewBag.UserRole = role;

            ViewBag.ProductCount = _context.Products.Count();
            ViewBag.CompanyCount = _context.Companies.Count(c => c.IsActive);
            ViewBag.InvoiceCount = _context.Invoices.Count();
            ViewBag.UserCount = _context.Users.Count();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
