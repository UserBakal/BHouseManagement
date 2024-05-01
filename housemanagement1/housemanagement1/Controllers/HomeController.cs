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

namespace housemanagement1.Controllers
{
    public class HomeController : BaseController
    {
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
        public ActionResult Reserve()
        {
            return View();
        }
        // Action to display the reservation form
        public ActionResult Reservations()
        {
            var reservations = _reservationRepo.GetAll();
            return View(reservations);
        }



        [HttpPost]
        public ActionResult Reserve(int roomId, DateTime startTime, DateTime endTime)
        {
        
            //int userId = GetCurrentUserId(); 

   
            Reservations reservation = new Reservations
            {
                startTime = startTime,
                endTime = endTime,
                status = "Pending", 
                //userId = id,
                roomId = roomId
            };

            var result = _reservationRepo.Create(reservation);

            if (result == ErrorCode.Success)
            {
                TempData["Msg"] = "Reservation added!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMsg"] = "Failed to add reservation.";
                return View("ReservationForm", reservation);
            }
        }
        public ActionResult Reserve(DateTime startTime, DateTime endTime, string status, int userId, int roomId)
        {
            // Create a new Reservations object
            Reservations reservation = new Reservations
            {
                startTime = startTime,
                endTime = endTime,
                status = status,
                userId = userId,
                roomId = roomId
            };

            // Save the reservation into the database
            var result = _reservationRepo.Create(reservation);

            if (result == ErrorCode.Success)
            {
                TempData["Msg"] = "Reservation added!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMsg"] = "Failed to add reservation.";
                return View(reservation);
            }
        }






        //[HttpPost]
        //public ActionResult Reserve(Reservations reservation)
        //{
        //    var result = _reservationRepo.Create(reservation);
        //    if (result == ErrorCode.Success)
        //    {
        //        TempData["Msg"] = "Reservation added!";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        TempData["ErrorMsg"] = "Failed to add reservation.";
        //        return View(reservation);
        //    }
        //}

        //public ActionResult Reservations()
        //{
        //    var reservations = _reservationRepo.GetAll();
        //    return View(reservations);
        //}

        //public ActionResult CancelReservation(int id)
        //{
        //    var result = _reservationRepo.Delete(id);
        //    if (result == ErrorCode.Success)
        //    {
        //        TempData["Msg"] = "Reservation canceled!";
        //        return RedirectToAction("Reservations");
        //    }
        //    else
        //    {
        //        TempData["ErrorMsg"] = "Failed to cancel reservation.";
        //        return RedirectToAction("Reservations");
        //    }
        //}



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
