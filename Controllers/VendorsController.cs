using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mn_you.Models.SQLite;
using mn_you.ViewModels.Vendors;

namespace mn_you.Controllers
{
    public class VendorsController : Controller
    {


        public IActionResult Index()
        {
            List<Vendor> vendors;
            using (var db = new MnyouContext()) {
                vendors = db.Vendors.OrderBy(v => v.Name).ToList();
            }
            return View(vendors);
        }

        public IActionResult Search(string q) {
            List<Vendor> vendors;
            using (var db = new MnyouContext()) {
                vendors = db.Vendors.Where(v => v.Name.Contains(q) || v.Bio.Contains(q)).ToList();
            }
            return View("Index", vendors);
        }

        public IActionResult Details(string id) {
            Vendor vendor;
            using(var db = new MnyouContext()) {
                vendor = db.Vendors.FirstOrDefault(v => v.Slug == id);
            }

            if (vendor == null) return RedirectToAction("Index");

            return View(vendor);
        }

        public IActionResult Create() {
            ViewBag.method = Request.Method;
            return View(new Vendor());
        }

        [HttpPostAttribute]
        public IActionResult Create(Vendor vendor) {
            ViewBag.method = Request.Method;
            if (ModelState.IsValid) {
                using (var db = new MnyouContext()) {
                    vendor.GenerateSlug();
                    db.Vendors.Add(vendor);
                    db.SaveChanges();
                }
                LogInVendor(vendor);
                return RedirectToAction("ThankYou");
            }
            return View(vendor);
        }


        [Authorize]
        public IActionResult Edit() {
            ViewBag.method = Request.Method;
            return View(GetLoggedInVendor());
        }

        [Authorize, HttpPostAttribute]
        public IActionResult Edit(Vendor postedVendor) {
            ViewBag.method = Request.Method;
            var vendor = GetLoggedInVendor();
            vendor.CopyValuesFrom(postedVendor);

            ModelState.Clear();
            TryValidateModel(vendor);

            if (ModelState.IsValid) {
                using(var db = new MnyouContext()) {
                    vendor.GenerateSlug();
                    db.Vendors.Update(vendor);
                    db.SaveChanges();
                }
                ModelState.AddModelError("base","Your changes have been saved.");
            }

            return View(vendor);
        }

        // [Authorize]
        public IActionResult ThankYou() {
            return View();
        }

        public IActionResult LogIn(LogInViewModel vm) {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index","Home");
            ViewBag.method = Request.Method;

            if (ViewBag.method == "POST") {
                using (var db = new MnyouContext()) {
                    var vendor = db.Vendors.FirstOrDefault(v => v.Email == vm.Email && v.Password == vm.Password);
                    if (vendor != null) {
                        LogInVendor(vendor);
                        return RedirectToAction("Details", new { id = vendor.Slug });
                    } else {
                        ModelState.AddModelError("base", "Your email address or password could not be found.");
                    }
                }

            }

            return View(vm);

        }

        public IActionResult LogOut() {
            HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
            return RedirectToAction("Index","Home");
        }


        private void LogInVendor(Vendor vendor) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.PrimarySid, vendor.VendorId.ToString()),
                new Claim(ClaimTypes.Name, vendor.Name)
            };
            var identity = new ClaimsIdentity(claims, "Basic");
            var principal = new ClaimsPrincipal(identity);
            HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", principal);
        }

        private Vendor GetLoggedInVendor() {
            using(var db = new MnyouContext()) {
                var userId = User.Claims.First(c => c.Type == ClaimTypes.PrimarySid).Value;
                return db.Vendors.First(v => v.VendorId == Int32.Parse(userId));
            }
        }
    }
}
