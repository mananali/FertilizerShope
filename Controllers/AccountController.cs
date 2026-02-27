using FertilizerShopWeb.Data;
using Microsoft.AspNetCore.Mvc;

//namespace FertilizerShopWeb.Controllers
//{
//    public class AccountController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}

//using Fertilizer_And_Seed_management_System.Data;
//using Microsoft.AspNetCore.Mvc;

//namespace Fertilizer_And_Seed_management_System.Controllers
namespace FertilizerShopWeb.Controllers
{
    public class AccountController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and Password required";
                return View();
            }

            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid Email or Password";
                return View();
            }

            // Session set
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.Name);

            // ✅ FIXED redirect
            if (user.Role == "Admin")
                return RedirectToAction("Index", "Product");
            else
                return RedirectToAction("Index", "Invoice");
        }

        // ---------------- LOGOUT ----------------
        public IActionResult Logout()
        {
            // সব session clear হবে
            HttpContext.Session.Clear();

            // Login page এ redirect
            return RedirectToAction("Login");
        }

    }
}

