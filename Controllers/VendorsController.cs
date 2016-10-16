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
                vendors = db.Vendors.ToList();
            }
            return View(vendors);
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
                return RedirectToAction("ThankYou");
            }
            return View(vendor);
        }


        [Authorize]
        public IActionResult Edit() {
            ViewBag.method = Request.Method;
            Vendor vendor;
            using(var db = new MnyouContext()) {
                var userId = User.Claims.First(c => c.Type == ClaimTypes.PrimarySid).Value;
                vendor = db.Vendors.First(v => v.VendorId == Int32.Parse(userId));
            }
            return View(vendor);
        }

        [Authorize, HttpPostAttribute]
        public IActionResult Edit(Vendor postedVendor) {
            ViewBag.method = Request.Method;
            Vendor vendor;
            using(var db = new MnyouContext()) {
                var userId = User.Claims.First(c => c.Type == ClaimTypes.PrimarySid).Value;
                vendor = db.Vendors.First(v => v.VendorId == Int32.Parse(userId));
                vendor.CopyValuesFrom(postedVendor);

                ModelState.Clear();
                TryValidateModel(vendor);
                if (ModelState.IsValid) {
                    vendor.GenerateSlug();
                    db.Vendors.Update(vendor);
                    db.SaveChanges();
                }
            }

            return View(vendor);
        }

        // [Authorize]
        public IActionResult ThankYou() {

            // User.Identity.Name
            ViewBag.foo = User.Claims;// ControllerContext.HttpContext.Authentication.GetAuthenticateInfoAsync("MyCookieMiddlewareInstance");
            // ViewBag.bar = User.Claims.First(c => c.Type == ClaimTypes.PrimarySid).Value;

            return View();
        }

        public IActionResult LogIn(LogInViewModel vm) {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index","Home");
            ViewBag.method = Request.Method;

            if (ViewBag.method == "POST") {
                using (var db = new MnyouContext()) {
                    var vendor = db.Vendors.FirstOrDefault(v => v.Email == vm.Email && v.Password == vm.Password);
                    if (vendor != null) {
                        ModelState.AddModelError("base","Test");
                        var claims = new List<Claim> {
                            new Claim(ClaimTypes.PrimarySid, vendor.VendorId.ToString()),
                            new Claim(ClaimTypes.Name, vendor.Name)
                        };
                        var identity = new ClaimsIdentity(claims, "Basic");
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", principal);
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
    }
}
