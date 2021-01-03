using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMDT_Web.Models.Domain;

namespace TMDT_Web.Models
{
    public class Item
    {
        public Product product { get; set; }
        public int QuantityBuy { get; set; }
        public int FinalTotal { get; set; }
        public Item(Product product, int quantitybuy, int finalTotal)
        {
            this.product = product;
            QuantityBuy= quantitybuy;
            FinalTotal = finalTotal;
        }
    }
}