using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mn_you.Models.SQLite;

namespace mn_you.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Vendor> vendors;
            using (var db = new MnyouContext()) {
                vendors = db.Vendors.Take(5).ToList();
            }
            return View(vendors);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Event()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Vender()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult SignUp()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Login()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
