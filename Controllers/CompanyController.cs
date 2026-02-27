//using FertilizerShopWeb.Data;
//using FertilizerShopWeb.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace FertilizerShopWeb.Controllers
//{
//    public class CompanyController : Controller
//    {
//        private readonly AppDbContext _context;

//        public CompanyController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // ---------------- INDEX ----------------
//        public IActionResult Index()
//        {
//            var userRole = HttpContext.Session.GetString("UserRole");

//            var companies = _context.Companies
//                                    .Include(c => c.Products)
//                                    .ToList();

//            ViewBag.UserRole = userRole;
//            return View(companies);
//        }

//        // ---------------- CREATE (GET) ----------------
//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // ---------------- CREATE (POST) ----------------
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Company company)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(company);
//            }

//            _context.Companies.Add(company);
//            _context.SaveChanges();

//            return RedirectToAction("Index");
//        }

//        // ---------------- DELETE ----------------
//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            var company = _context.Companies
//                                  .Include(c => c.Products)
//                                  .FirstOrDefault(c => c.CompanyId == id);

//            if (company != null)
//            {
//                _context.Companies.Remove(company);
//                _context.SaveChanges();
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}

using FertilizerShopWeb.Data;
using FertilizerShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FertilizerShopWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly AppDbContext _context;

        public CompanyController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------- INDEX ----------------
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var companies = _context.Companies
                                    .Include(c => c.Products)
                                    .ToList();

            ViewBag.UserRole = userRole;
            return View(companies);
        }

        // ---------------- CREATE (GET) ----------------
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ---------------- CREATE (POST) ----------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company company)
        {
            if (!ModelState.IsValid) return View(company);

            _context.Companies.Add(company);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ---------------- EDIT (GET) ----------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null) return NotFound();

            return View(company);
        }

        // ---------------- EDIT (POST) ----------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company company)
        {
            if (!ModelState.IsValid) 
                return View(company);

            var existing = _context.Companies.Find(company.CompanyId);
            if (existing == null) return NotFound();

            // Update fields
            existing.CompanyName = company.CompanyName;
            existing.CompanyType = company.CompanyType;
            existing.ContactPerson = company.ContactPerson;
            existing.ContactNumber = company.ContactNumber;
            existing.Email = company.Email;
            existing.Address = company.Address;
            existing.IsActive = company.IsActive;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ---------------- DELETE ----------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var company = _context.Companies
                                  .Include(c => c.Products)
                                  .FirstOrDefault(c => c.CompanyId == id);

            if (company != null)
            {
                _context.Companies.Remove(company);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
