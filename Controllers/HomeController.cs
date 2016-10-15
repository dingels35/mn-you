using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mn_you.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
