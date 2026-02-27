////////using FertilizerShopWeb.Data;
////////using FertilizerShopWeb.Models;
////////using Microsoft.AspNetCore.Mvc;
////////using Microsoft.EntityFrameworkCore;
////////using System.Linq;
////////using Microsoft.AspNetCore.Http;

////////namespace FertilizerShopWeb.Controllers
////////{
////////    public class ProductController : Controller
////////    {
////////        private readonly AppDbContext _context;

////////        public ProductController(AppDbContext context)
////////        {
////////            _context = context;
////////        }
////////        private bool CheckAccess(string role)
////////        {
////////            var userRole = HttpContext.Session.GetString("UserRole");

////////            if (string.IsNullOrEmpty(userRole))
////////                return false;

////////            return userRole == role;
////////        }


////////        // GET: Product
////////        public IActionResult Index()
////////        {
////////            if (HttpContext.Session.GetString("UserId") == null)
////////            {
////////                return RedirectToAction("Login", "Account");
////////            }


////////            var products = _context.Products.ToList();
////////            return View(products);   // ✅ এখানেই fix
////////        }

////////        // GET: Product/Create
////////        public IActionResult Create()
////////        {
////////            return View();
////////        }

////////        // POST: Product/Create
////////        [HttpPost]
////////        public IActionResult Create(Product product)
////////        {
////////            if (ModelState.IsValid)
////////            {
////////                _context.Products.Add(product);
////////                _context.SaveChanges();
////////                return RedirectToAction("Index");
////////            }
////////            ViewBag.Companies = _context.Companies.ToList();
////////            return View(product);

////////        }

////////        // GET: Product/Edit/5
////////        public IActionResult Edit(int id)
////////        {
////////            var product = _context.Products.Find(id);
////////            if (product == null)
////////            {
////////                return NotFound();
////////            }
////////            return View(product);
////////        }

////////        // POST: Product/Edit
////////        [HttpPost]
////////        public IActionResult Edit(Product product)
////////        {
////////            if (ModelState.IsValid)
////////            {
////////                _context.Products.Update(product);
////////                _context.SaveChanges();
////////                return RedirectToAction("Index");
////////            }
////////            return View(product);
////////        }

////////        // GET: Product/Delete/5
////////        public IActionResult Delete(int id)
////////        {
////////            var product = _context.Products.Find(id);
////////            if (product != null)
////////            {
////////                _context.Products.Remove(product);
////////                _context.SaveChanges();
////////            }
////////            return RedirectToAction("Index");
////////        }

////////    }
////////}



//////using FertilizerShopWeb.Data;
//////using FertilizerShopWeb.Models;
//////using Microsoft.AspNetCore.Mvc;
//////using Microsoft.EntityFrameworkCore;
//////using Microsoft.AspNetCore.Http;
//////using System.Linq;

//////namespace FertilizerShopWeb.Controllers
//////{
//////    public class ProductController : Controller
//////    {
//////        private readonly AppDbContext _context;

//////        public ProductController(AppDbContext context)
//////        {
//////            _context = context;
//////        }

//////        // GET: Product
//////        public IActionResult Index()
//////        {
//////            if (HttpContext.Session.GetString("UserId") == null)
//////            {
//////                return RedirectToAction("Login", "Account");
//////            }

//////            // Include company to show company name in index
//////            var products = _context.Products.Include(p => p.Company).ToList();
//////            return View(products);
//////        }

//////        // GET: Product/Create
//////        public IActionResult Create()
//////        {
//////            // Companies dropdown
//////            //ViewBag.Companies = _context.Companies.ToList();
//////            //return View();
//////            ViewBag.Companies = _context.Companies
//////                               .Where(c => c.IsActive) // IsActive = true
//////                               .ToList();
//////            return View();

//////        }

//////        // POST: Product/Create
//////        [HttpPost]
//////        [ValidateAntiForgeryToken]
//////        public IActionResult Create(Product product)
//////        {
//////            //if (ModelState.IsValid)
//////            //{
//////            //    _context.Products.Add(product);
//////            //    _context.SaveChanges();
//////            //    return RedirectToAction("Index");
//////            //}

//////            //// ModelState invalid হলে আবার dropdown
//////            //ViewBag.Companies = _context.Companies.ToList();
//////            //return View(product);
//////            //ViewBag.Companies = _context.Companies
//////            //                    .Where(c => c.IsActive)
//////            //                    .ToList();

//////            //if (ModelState.IsValid)
//////            //{
//////            //    _context.Products.Add(product);
//////            //    _context.SaveChanges();
//////            //    return RedirectToAction("Index");
//////            //}

//////            //// validation fail হলে view ফেরত দাও product model সহ
//////            //return View(product);
//////            ViewBag.Companies = _context.Companies
//////                                .Where(c => c.IsActive)
//////                                .ToList();

//////            if (!ModelState.IsValid)
//////                return View(product);

//////            _context.Products.Add(product);
//////            _context.SaveChanges();
//////            return RedirectToAction("Index");

//////        }
//////        // GET: Product/Edit/5
//////        public IActionResult Edit(int id)
//////        {
//////            var product = _context.Products.Find(id);
//////            if (product == null) return NotFound();

//////            // Companies dropdown
//////            ViewBag.Companies = _context.Companies.ToList();
//////            return View(product);
//////        }

//////        // POST: Product/Edit
//////        [HttpPost]
//////        public IActionResult Edit(Product product)
//////        {
//////            if (ModelState.IsValid)
//////            {
//////                _context.Products.Update(product);
//////                _context.SaveChanges();
//////                return RedirectToAction("Index");
//////            }

//////            // Invalid হলে dropdown আবার পাঠানো
//////            ViewBag.Companies = _context.Companies.ToList();
//////            return View(product);
//////        }

//////        // GET: Product/Delete/5
//////        public IActionResult Delete(int id)
//////        {
//////            var product = _context.Products.Find(id);
//////            if (product != null)
//////            {
//////                _context.Products.Remove(product);
//////                _context.SaveChanges();
//////            }
//////            return RedirectToAction("Index");
//////        }
//////    }
//////}


////using FertilizerShopWeb.Data;
////using FertilizerShopWeb.Models;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Http;
////using Microsoft.EntityFrameworkCore;
////using System.Linq;

////namespace FertilizerShopWeb.Controllers
////{
////    public class ProductController : Controller
////    {
////        private readonly AppDbContext _context;

////        public ProductController(AppDbContext context)
////        {
////            _context = context;
////        }

////        // GET: Product
////        public IActionResult Index()
////        {
////            if (HttpContext.Session.GetString("UserId") == null)
////                return RedirectToAction("Login", "Account");

////            var products = _context.Products.Include(p => p.Company).ToList();
////            return View(products);
////        }

////        // GET: Product/Create
////        public IActionResult Create()
////        {
////            ViewBag.Companies = _context.Companies
////                                        .Where(c => c.IsActive)
////                                        .ToList();
////            return View();
////        }

////        // POST: Product/Create
////        [HttpPost]
////        [ValidateAntiForgeryToken]
////        public IActionResult Create(Product product)
////        {
////            ViewBag.Companies = _context.Companies
////                                        .Where(c => c.IsActive)
////                                        .ToList();

////            if (!ModelState.IsValid)
////                return View(product);

////            _context.Products.Add(product);
////            _context.SaveChanges();
////            return RedirectToAction("Index");
////        }

////        // GET: Product/Edit/5
////        public IActionResult Edit(int id)
////        {
////            var product = _context.Products.Find(id);
////            if (product == null) return NotFound();

////            ViewBag.Companies = _context.Companies.ToList();
////            return View(product);
////        }

////        // POST: Product/Edit
////        [HttpPost]
////        [ValidateAntiForgeryToken]
////        public IActionResult Edit(Product product)
////        {
////            ViewBag.Companies = _context.Companies.ToList();

////            if (!ModelState.IsValid)
////                return View(product);

////            _context.Products.Update(product);
////            _context.SaveChanges();
////            return RedirectToAction("Index");
////        }

////        // GET: Product/Delete/5
////        public IActionResult Delete(int id)
////        {
////            var product = _context.Products.Find(id);
////            if (product != null)
////            {
////                _context.Products.Remove(product);
////                _context.SaveChanges();
////            }
////            return RedirectToAction("Index");
////        }
////    }
////}

//using FertilizerShopWeb.Data;
//using FertilizerShopWeb.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Http;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace FertilizerShopWeb.Controllers
//{
//    public class ProductController : Controller
//    {
//        private readonly AppDbContext _context;

//        public ProductController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Product
//        public IActionResult Index()
//        {
//            if (HttpContext.Session.GetString("UserId") == null)
//                return RedirectToAction("Login", "Account");

//            var products = _context.Products
//                                   .Include(p => p.Company)
//                                   .ToList();
//            return View(products);
//        }

//        // GET: Product/Create
//        public IActionResult Create()
//        {
//            // Send active companies to view
//            ViewBag.Companies = _context.Companies
//                                        .Where(c => c.IsActive)
//                                        .ToList();
//            return View();
//        }

//        // POST: Product/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(Product product)
//        {
//            // always populate companies for dropdown
//            ViewBag.Companies = _context.Companies
//                                        .Where(c => c.IsActive)
//                                        .ToList();

//            if (!ModelState.IsValid)
//                return View(product);

//            _context.Products.Add(product);
//            _context.SaveChanges();
//            return RedirectToAction("Index");
//        }




//        // GET: Product/Edit/5
//        public IActionResult Edit(int id)
//        {
//            var product = _context.Products.Find(id);
//            if (product == null) return NotFound();

//            ViewBag.Companies = _context.Companies
//                                        .Where(c => c.IsActive)
//                                        .ToList();
//            return View(product);
//        }

//        // POST: Product/Edit
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(Product product)
//        {
//            ViewBag.Companies = _context.Companies
//                                        .Where(c => c.IsActive)
//                                        .ToList();

//            if (!ModelState.IsValid)
//                return View(product);

//            _context.Products.Update(product);
//            _context.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        // GET: Product/Delete/5
//        public IActionResult Delete(int id)
//        {
//            var product = _context.Products.Find(id);
//            if (product != null)
//            {
//                _context.Products.Remove(product);
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace FertilizerShopWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");

            var products = _context.Products
                                   .Include(p => p.Company)
                                   .ToList();
            return View(products);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            PopulateCompaniesDropdown();
            return View();
        }

        // POST: Product/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Product product)
        //{
        //    PopulateCompaniesDropdown();

        //    if (!ModelState.IsValid)
        //        return View(product);

        //    _context.Products.Add(product);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            PopulateCompaniesDropdown();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                ViewBag.Errors = errors;
                return View(product);
            }

            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.DbError = ex.Message;
                return View(product);
            }

            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        //public IActionResult Edit(int id)
        //{
        //    var product = _context.Products.Find(id);
        //    if (product == null) return NotFound();

        //    PopulateCompaniesDropdown(product.CompanyId);
        //    return View(product);
        //}
        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _context.Products
                                  .Include(p => p.Company)
                                  .FirstOrDefault(p => p.ProductId == id);

            if (product == null) return NotFound();

            // Company dropdown
            ViewBag.Companies = _context.Companies
                                        .Where(c => c.IsActive)
                                        .ToList();

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Companies = _context.Companies
                                            .Where(c => c.IsActive)
                                            .ToList();
                return View(model);
            }

            var product = _context.Products.FirstOrDefault(p => p.ProductId == model.ProductId);
            if (product == null) return NotFound();

            // ✅ Assign safe values
            product.ProductName = model.ProductName;
            product.ProductType = model.ProductType;
            product.Price = model.Price;
            product.StockQty = model.StockQty;

            // Check if selected Company exists
            var company = _context.Companies.FirstOrDefault(c => c.CompanyId == model.CompanyId && c.IsActive);
            if (company == null)
            {
                ModelState.AddModelError("CompanyId", "Selected company is invalid or inactive");
                ViewBag.Companies = _context.Companies
                                            .Where(c => c.IsActive)
                                            .ToList();
                return View(model);
            }

            product.CompanyId = company.CompanyId;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // 🔹 Helper: Populate companies for dropdown
        private void PopulateCompaniesDropdown(int selectedCompanyId = 0)
        {
            var companies = _context.Companies
                                    .Where(c => c.IsActive)
                                    .ToList() ?? new List<Company>();

            ViewBag.Companies = new SelectList(companies, "CompanyId", "CompanyName", selectedCompanyId);
        }
    }
}

