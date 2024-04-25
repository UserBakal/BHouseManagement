using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace housemanagement1.Models
{
    namespace housemanagement1.Models
    {
        public class Reservation
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }

}