using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace housemanagement1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home 
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult Create(users u)
        //{
        //    _userRepo.Create(u);
        //    TempData["Msg"] = $"User {u.userName} Added!";

        //    return RedirectToAction("Login");
        //}
    }
}