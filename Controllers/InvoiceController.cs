

using FertilizerShopWeb.Data;
using FertilizerShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace FertilizerShopWeb.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly AppDbContext _context;

        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------- INDEX ----------------
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");

            var invoices = _context.Invoices
                .Include(i => i.Items)
                .OrderByDescending(i => i.InvoiceDate)
                .ToList();

            return View(invoices);
        }

        // ---------------- DETAILS ----------------
        public IActionResult Details(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null) return NotFound();

            return View(invoice);
        }

        // ---------------- CREATE (GET) ----------------
        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin()) return AccessDenied();

            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // ---------------- CREATE (POST) ----------------
      
        
        [HttpPost]
        public IActionResult Create(int ProductId, decimal Qty, string CustomerName, string CustomerContact)
        {
            if (!IsAdmin()) return AccessDenied();

            var product = _context.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (product == null)
                return Content("Product not found");

            if (product.StockQty < Qty)
            {
                ViewBag.Error = "Stock not sufficient!";
                ViewBag.Products = _context.Products.ToList();
                return View();
            }

            var invoice = new Invoice
            {
                InvoiceNo = "INV-" + DateTime.Now.Ticks,
                InvoiceDate = DateTime.Now,
                CustomerName = CustomerName,
                CustomerContact = CustomerContact,
                Items = new List<InvoiceItem>()
            };

            var item = new InvoiceItem
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Qty = Qty,
                Rate = product.Price
            };

            invoice.Items.Add(item);

            invoice.SubTotal = item.Amount;
            invoice.Vat = 0;
            invoice.GrandTotal = invoice.SubTotal;

            product.StockQty -= Qty;

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // ---------------- EDIT (GET) ----------------


        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    if (!IsAdmin()) return AccessDenied();

        //    var invoice = _context.Invoices
        //        .Include(i => i.Items)
        //        .FirstOrDefault(i => i.InvoiceId == id);

        //    if (invoice == null) return NotFound();

        //    // Initialize items if null
        //    if (invoice.Items == null)
        //        invoice.Items = new List<InvoiceItem>();

        //    ViewBag.Products = _context.Products.ToList();
        //    return View(invoice);
        //}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null) return NotFound();

            // Make sure Items is never null
            if (invoice.Items == null || invoice.Items.Count == 0)
                invoice.Items = new List<InvoiceItem> { new InvoiceItem() };

            // ✅ Populate dropdown
            ViewBag.ProductList = new SelectList(_context.Products, "ProductId", "ProductName");

            return View(invoice);
        }


        // ---------------- EDIT (POST) ----------------


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Invoice invoice)
        //{
        //    if (!IsAdmin()) return AccessDenied();

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Products = _context.Products.ToList();
        //        return View(invoice);
        //    }

        //    var existingInvoice = _context.Invoices
        //        .Include(i => i.Items)
        //        .FirstOrDefault(i => i.InvoiceId == invoice.InvoiceId);

        //    if (existingInvoice == null) return NotFound();

        //    // Update customer info
        //    existingInvoice.CustomerName = invoice.CustomerName;
        //    existingInvoice.CustomerContact = invoice.CustomerContact;

        //    // Remove old items
        //    _context.InvoiceItems.RemoveRange(existingInvoice.Items);

        //    // Add updated items
        //    existingInvoice.Items = invoice.Items;

        //    // Recalculate totals
        //    existingInvoice.SubTotal = existingInvoice.Items.Sum(i => i.Qty * i.Rate);
        //    existingInvoice.Vat = invoice.Vat;
        //    existingInvoice.GrandTotal = existingInvoice.SubTotal + invoice.Vat;

        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Invoice invoice)
        {
            if (!IsAdmin()) return AccessDenied();

            if (!ModelState.IsValid)
            {
                ViewBag.Products = _context.Products.ToList();
                return View(invoice);
            }

            var existingInvoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.InvoiceId == invoice.InvoiceId);

            if (existingInvoice == null) return NotFound();

            // Update customer info
            existingInvoice.CustomerName = invoice.CustomerName;
            existingInvoice.CustomerContact = invoice.CustomerContact;
            existingInvoice.Vat = invoice.Vat;

            // Remove old items
            _context.InvoiceItems.RemoveRange(existingInvoice.Items);
            _context.SaveChanges();

            // Add updated items
            foreach (var item in invoice.Items)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (product != null)
                {
                    existingInvoice.Items.Add(new InvoiceItem
                    {
                        ProductId = item.ProductId,
                        ProductName = product.ProductName,
                        Qty = item.Qty,
                        Rate = item.Rate
                    });
                }
            }

            // Recalculate totals
            existingInvoice.SubTotal = existingInvoice.Items.Sum(i => i.Qty * i.Rate);
            existingInvoice.GrandTotal = existingInvoice.SubTotal + existingInvoice.Vat;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // ---------------- DELETE ----------------
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return AccessDenied();

            var invoice = _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null) return NotFound();

            // Delete invoice items first
            _context.InvoiceItems.RemoveRange(invoice.Items);
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ---------------- PDF ----------------
        public IActionResult ExportPdf()
        {
            //    if (!IsAdmin()) return AccessDenied();

            //    var invoices = _context.Invoices
            //        .Include(i => i.Items)
            //        .ToList();

            //    return new ViewAsPdf("PdfView", invoices)
            //    {
            //        FileName = "Invoices.pdf"
            //    };
            if (!IsAdmin()) return AccessDenied();

        var invoices = _context.Invoices
                               .Include(i => i.Items)
                               .ThenInclude(it => it.Product)
                               .ToList();

        return new ViewAsPdf("PdfView", invoices)
        {
            FileName = "AllInvoices.pdf",
            PageSize = Rotativa.AspNetCore.Options.Size.A4
        };

        }

        // ---------------- UTIL ----------------
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }

        private IActionResult AccessDenied()
        {
            return Content("Access Denied! Admin only.");
        }
    }
}






