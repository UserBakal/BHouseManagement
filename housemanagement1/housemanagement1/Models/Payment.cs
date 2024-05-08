using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace housemanagement1.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string CardHolderName { get; set; }
        public decimal PaymentAmount { get; set; }
        public int ExpiryMonth { get; set; }

    }

}