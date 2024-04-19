using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using housemanagement1.Repository;

namespace housemanagement1.Controllers
{
    public class BaseController : Controller
    {
        public bhousemanagementEntities1 _db;
        public BaseRepository1<users> _userRepo;

        public BaseController()
        {
            _db = new bhousemanagementEntities1();
            _userRepo = new BaseRepository1<users>();   
        }

    }
}