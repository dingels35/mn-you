using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mn_you.Models.SQLite;

namespace mn_you.Controllers
{
    public class VendorsController : Controller
    {
        public IActionResult Index()
        {
            List<Vendor> vendors;
            using (var db = new MnyouContext()) {
                vendors = db.Vendors.ToList();
            }
            return View(vendors);
        }

        public IActionResult Create(Vendor vendor) {
            ViewBag.method = Request.Method;
            if (ModelState.IsValid) {
                using (var db = new MnyouContext()) {
                    db.Vendors.Add(vendor);
                    db.SaveChanges();
                }
                return RedirectToAction("ThankYou");
            }
            return View(vendor);

        }

        public IActionResult ThankYou() {
            return View();
        }
    }
}
