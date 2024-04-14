using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace housemanagement1.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            List<users> userList = _userRepo.GetAll();
            return View(userList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(users u)
        {
            _userRepo.Create(u);
            TempData["Msg"] = $"User {u.username} added!";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id) // Change: id parameter is now nullable
        {
            if (id == null)
            {
                TempData["ErrorMsg"] = "User ID is required.";
                return RedirectToAction("Index");
            }

            var user = _userRepo.Get(id.Value);

            if (user == null)
            {
                TempData["ErrorMsg"] = "User not found.";
                return RedirectToAction("Index");
            }

            return View(user);
        }
        public ActionResult Edit(int id)
        {
            return View(_userRepo.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(users u)
        {
            _userRepo.Update(u.id, u);
            TempData["Msg"] = $"User {u.username} Updated!";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            _userRepo.Delete(id);
            TempData["Msg"] = $"User Deleted!";

            return RedirectToAction("Index");
        }
    }
}