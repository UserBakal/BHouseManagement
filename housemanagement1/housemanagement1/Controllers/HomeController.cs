using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using housemanagement1.Models;
using System;
using System.Web.Security;
using housemanagement1.Repository;
using System.Linq;
using housemanagement1.Contracts;
using System.Data.Entity;
using System.Web.Helpers;
//using housemanagement1.Models;

namespace housemanagement1.Controllers
{
    public class HomeController : BaseController
    {
        private bhousemanagementEntities db = new bhousemanagementEntities();

        private readonly BaseRepository1<Reservations> _reservationRepo;

        public HomeController()
        {
            _reservationRepo = new BaseRepository1<Reservations>();
        }
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

        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(users u)
        {
            
            var user = _userRepo.Table().FirstOrDefault(m => m.username == u.username && m.password == u.password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(u.username, false);
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "User does not exist or incorrect password!");

            return View(u);
        }

        public ActionResult Dashboard(string username)
        {
            ViewBag.Username = username;
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserve(Reservations reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
  
                    string username = reservation.UserName;


                    reservation.Status = "Pending";


                    db.Reservations.Add(reservation);
                    db.SaveChanges();

                    TempData["Msg"] = "Reservation successfully saved!";
                    return RedirectToAction("ReservationSuccess");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the reservation.");

                    return View(reservation);
                }
            }
            else
            {

                return View("Index", reservation);
            }
        }



        public ActionResult ReservationSuccess()
        {
            return View();
        }











        public ActionResult Details(int? id) 
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
