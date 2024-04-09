using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using housemanagement1.Repository;

namespace housemanagement1.Controllers
{
    public class BaseController
    {
        public BhouseManagementEntities _db;
        public BaseRepository<users> _userRepo;
        // GET: Base
        public BaseController()
        {
            _db = new BhouseManagementEntities();
            _userRepo = new BaseRepository<users>();
        }
    }
}