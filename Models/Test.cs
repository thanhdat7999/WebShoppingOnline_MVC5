using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models
{
    public class Test:PayPal.Api.Item
    {
        public int discount { get; set; }
        public PayPal.Api.Item payPal { get; set; }
    }
}