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
using housemanagement1.Models;

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

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var user = _userRepo.Table().FirstOrDefault(m => m.username == model.Username && m.password == model.Password);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("Dashboard", new { username = model.Username });
            }

            // Check if the user is an admin
            var adminAccount = db.AdminAccounts.FirstOrDefault(a => a.Username == model.Username);
            if (adminAccount != null && Crypto.VerifyHashedPassword(adminAccount.Password, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("AdminDashboard");
            }

            ModelState.AddModelError("", "User does not exist or incorrect password!");

            return View(model);
        }


        public ActionResult Dashboard(string username)
        {
            ViewBag.Username = username;
            return View();
        }


        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminLogin admin)
        {
            var adminAccount = db.AdminAccounts.FirstOrDefault(a => a.Username == admin.Username);

            if (adminAccount != null && Crypto.VerifyHashedPassword(adminAccount.Password, admin.Password))
            {
                FormsAuthentication.SetAuthCookie(admin.Username, false);
                return RedirectToAction("AdminDashboard");
            }

            ModelState.AddModelError("", "Invalid admin credentials");
            return View(admin);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserve(Reservations reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Get the username from the form
                    string username = reservation.UserName;

                    // Assigning the username and setting status to Pending
                    reservation.Status = "Pending";

                    // Adding the reservation to the database
                    db.Reservations.Add(reservation);
                    db.SaveChanges();

                    TempData["Msg"] = "Reservation successfully saved!";
                    return RedirectToAction("ReservationSuccess");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the reservation.");
                    // Log the exception if needed
                    return View(reservation); // Return to the form with errors
                }
            }
            else
            {
                // Model state is not valid, return to the form with validation errors
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
