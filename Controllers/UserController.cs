using FertilizerShopWeb.Data;
using FertilizerShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace FertilizerShopWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: User/Register
        public IActionResult Register()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role == "Admin")
            {
                // Admin can create Admin or Staff
                ViewBag.Roles = new List<string> { "Admin", "Staff" };
            }
            else if (role == "Staff")
            {
                // Staff can only create Staff
                ViewBag.Roles = new List<string> { "Staff" };
            }
            else
            {
                // Guest / not logged in
                ViewBag.Roles = new List<string> { "Staff" }; // default role
            }

            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate role dropdown
                var role = HttpContext.Session.GetString("UserRole");
                if (role == "Admin")
                    ViewBag.Roles = new List<string> { "Admin", "Staff" };
                else
                    ViewBag.Roles = new List<string> { "Staff" };

                return View(user);
            }

            // Optional: hash password here
            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "User registered successfully!";
            return RedirectToAction("Index", "Home");
        }

        // GET: User/Index (Admin view all users)
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
                return RedirectToAction("AccessDenied", "Account"); // only Admin can see users

            var users = _context.Users.ToList();
            return View(users);
        }
    }
}
