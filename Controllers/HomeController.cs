using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogReg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LogReg.Controllers
{
    public class HomeController : Controller
    {
        private LogRegContext dbContext;

        public HomeController(LogRegContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Reg")]
        public IActionResult Reg(RUser NewUse)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.RUsers.Any(u => u.Email == NewUse.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use!");
                    return View("Index");
                }
                PasswordHasher<RUser> Hasher = new PasswordHasher<RUser>();
                NewUse.Password = Hasher.HashPassword(NewUse, NewUse.Password);
                dbContext.Add(NewUse);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("User", NewUse.RUserId);
                int? Sess = HttpContext.Session.GetInt32("User");
                return Redirect("/Success");
            } else {
                return View("Index");
            }
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            var check = HttpContext.Session.GetInt32("User");
            if(check == null)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("logCheck")]
        public IActionResult logCheck(LUser use)
        {
            if(ModelState.IsValid)
            {
                var userCheck = dbContext.RUsers.FirstOrDefault(u => u.Email == use.Email);
                if(userCheck == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("login");
                }
                var hasher = new PasswordHasher<LUser>();
                var result = hasher.VerifyHashedPassword(use, userCheck.Password, use.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("login");
                }
                HttpContext.Session.SetInt32("User", use.LUId);
                int? Sess = HttpContext.Session.GetInt32("User");
                return Redirect("/Success");
            }
            return View("login");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
