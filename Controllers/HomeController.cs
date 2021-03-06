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
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult Event()
        {
            ViewData["Message"] = "Event Calender";

            return View();
        }

        public IActionResult Categories()
        {
            ViewData["Message"] = "Search by Categories";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
