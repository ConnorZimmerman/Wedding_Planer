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
    public class WeddingController : Controller
    {
        public wedding_plannerContext _context;

        public WeddingController(wedding_plannerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult WeddingDash()
        {
            int? num = HttpContext.Session.GetInt32("userId");
            if (num == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userId = (int)num;
            List<weddings> allWeddings = _context.weddings.Include(w => w.weddings_has_users).ThenInclude(u => u.user)
            .OrderBy(w => w.date).ToList();
            for (var i = 0; i < allWeddings.Count; i++)
            {
                if (allWeddings[i].date < DateTime.Now)
                {
                    DeleteWedding(allWeddings[i].id);
                }
            }
            ViewBag.userId = num;
            return View("WeddingDash", allWeddings);
        }

        [HttpGet]
        [Route("plan_wedding")]
        public IActionResult PlanWedding()
        {
            return View("PlanWedding");
        }

        [HttpPost]
        [Route("validate_wedding")]
        public IActionResult ValidateWedding(WeddingViewModels weddingMod)
        {
            if (ModelState.IsValid)
            {
                weddings wedding = CreateWedding(weddingMod);
                int id = wedding.id;
                return Redirect($"wedding_display/{id}");
            }
            return View("PlanWedding");
        }

        public weddings CreateWedding(WeddingViewModels weddingMod)
        {
            string wedders = $"{weddingMod.wedder_one} & {weddingMod.wedder_two}";
            int? userId = HttpContext.Session.GetInt32(key: "userId");
            int user_id = (int)userId;
            weddings addWedding = new weddings
            {
                wedders = wedders,
                date = weddingMod.Date,
                address = weddingMod.wedding_address,
                guests = 0,
                created_by = user_id
            };
            _context.Add(addWedding);
            _context.SaveChanges();
            return addWedding;
        }

        [HttpGet]
        [Route("wedding_display/{id}")]
        public IActionResult DisplayWedding(int id)
        {
            weddings wedding = _context.weddings.Where(w => w.id == id).Include(w => w.weddings_has_users).ThenInclude(u => u.user).SingleOrDefault();
            ViewBag.address = wedding.address;
            ViewBag.wedders = wedding.wedders;
            ViewBag.date = wedding.date;
            return View("DisplayWedding", wedding);
        }

        [HttpGet]
        [Route("AddUserToEvent/{weddingId}")]
        public IActionResult AddUserToEvent(int weddingId)
        {
            int? userId = HttpContext.Session.GetInt32(key: "userId");
            int user_id = (int)userId;
            users user = _context.users.Where(u => u.id == user_id).SingleOrDefault();
            weddings wedding = _context.weddings.Where(w => w.id == weddingId).SingleOrDefault();
            wedding.guests += 1;
            weddings_has_users addGuest = new weddings_has_users
            {
                weddings_id = wedding.id,
                users_id = user_id
            };
            _context.Add(addGuest);
            _context.SaveChanges();
            return RedirectToAction("WeddingDash");
        }

        [HttpGet]
        [Route("RemoveUserFromEvent/{weddingId}")]
        public IActionResult RemoveUserFromEvent(int weddingId)
        {
            int? userId = HttpContext.Session.GetInt32(key: "userId");
            int user_id = (int)userId;
            weddings wedding = _context.weddings.Where(w => w.id == weddingId).SingleOrDefault();
            weddings_has_users wedding_user = _context.weddings_has_users.Where(w => w.users_id == user_id).Where(w => w.weddings_id == weddingId).SingleOrDefault();
            wedding.guests -= 1;
            _context.weddings_has_users.Remove(wedding_user);
            _context.SaveChanges();
            return RedirectToAction("WeddingDash");
        }

        [HttpGet]
        [Route("delete/{weddingId}")]
        public IActionResult DeleteWedding(int weddingId)
        {
            int? num = HttpContext.Session.GetInt32("userId");
            if (num == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int user_id = (int)num;
            weddings wedding = _context.weddings.Where(w => w.id == weddingId).SingleOrDefault();
            if(user_id != wedding.created_by)
            {
                return RedirectToAction("Index", "Home");
            }
            _context.weddings.Remove(wedding);
            _context.SaveChanges();
            return RedirectToAction("WeddingDash");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}