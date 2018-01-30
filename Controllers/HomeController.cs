using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using wedding_planner.Models;

namespace wedding_planner.Controllers
{
    public class HomeController : Controller
    {
        public wedding_plannerContext _context;

        public HomeController(wedding_plannerContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("ValidateRegistration")]
        public IActionResult RegisterUser(RegisterViewModels userAdd)
        {
            if (ModelState.IsValid)
            {
                CreateUser(userAdd);
                int? userIdQ = HttpContext.Session.GetInt32(key: "userId");
                int userId = (int)userIdQ;
                return RedirectToAction("WeddingDash", "Wedding");
            }
            return View("Index");
        }

        public void CreateUser(RegisterViewModels userAdd)
        {
            users user = new users
            {
                first_name = userAdd.first_name,
                last_name = userAdd.last_name,
                email = userAdd.email,
                password = userAdd.password,
            };
            _context.Add(user);
            _context.SaveChanges();
            SelectUser(userAdd);
        }

        public void SelectUser(RegisterViewModels useradd)
        {
            users user = _context.users.Where(u => u.email == useradd.email).Single();
            HttpContext.Session.SetInt32(key: "userId", value: user.id);
        }

        [HttpPost]
        [Route("login")]

        public IActionResult LoginUser(LoginViewModels loginView)
        {
            if (ModelState.IsValid)
            {
                users user = _context.users.Where(u => u.email == loginView.lEmail).SingleOrDefault();
                if (user != null && user.password == loginView.lPassword)
                {
                    HttpContext.Session.SetInt32(key: "userId", value: user.id);
                    return RedirectToAction("WeddingDash", "Wedding");
                }
                ViewBag.error = "Information does not match any account in the database";
            }
            return View("Index");
        }
    }
}
