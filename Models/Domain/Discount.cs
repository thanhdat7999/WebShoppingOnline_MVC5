using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.Domain
{
    public class Discount
    {
        [Key]
        public int EventID { get; set; }
        public string Event { get; set; }
        public int DiscountPercent { get; set; }
        public int Quantity { get; set; }
        public string CodeRnd { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
    }
}