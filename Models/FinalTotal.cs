using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models
{
    public class FinalTotal
    {
        public int finalTotal { get; set; }
        public FinalTotal(int final)
        {
            finalTotal = final;
        }

        public FinalTotal()
        {
        }
    }
}