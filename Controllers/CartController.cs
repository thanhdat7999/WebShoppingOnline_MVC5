using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;
using TMDT_Web.Models;
using TMDT_Web.Models.Domain;

namespace TMDT_Web.Controllers
{
    public class CartController : Controller
    {
        DataContext db = new DataContext();
        // GET: Cart

        //Trang giỏ hàng
        public ActionResult Index()
        {
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if(Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }
            
            //Kiểm tra giỏ hàng rỗng hay đã có sản phẩm rồi
            if (Session["cart"] == null)
            {
                cart = new List<Item>();
                Session["cart"] = cart;
            }
            else
            {
                var total = cart.Sum(x => x.QuantityBuy * x.product.Price);
                ViewBag.total = total;
            }
            return View();
        }

        //Thêm sản phẩm vào giỏ hàng
        public ActionResult Buy(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { product = db.product.FirstOrDefault(x => x.ProductID == id), QuantityBuy = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].QuantityBuy++;
                }
                else
                {
                    cart.Add(new Item { product = db.product.Find(id), QuantityBuy = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("menu","home");
        }

        //Xóa từng sản phẩm khỏi giỏ hàng
        public ActionResult Remove(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("index");
        }

        //Đặt order sản phẩm
        public ActionResult CheckOut(int? num)
        {
            List<Item> cart = (List<Item>)Session["cart"];

            //Kiểm tra nếu người dùng có tài khoản thêm sp vào giỏ hàng
            if (User.Identity.IsAuthenticated)
            {
                //Orders
                var acc = db.account.FirstOrDefault(x => x.UserName == User.Identity.Name);
                var order = new Order();
                order.UserID = acc.UserID;
                order.DateTimeOrder = DateTime.Now;
                order.Status = null;
                order.OrderPhoneNumber = acc.PhoneNumber;
                db.order.Add(order);
                db.SaveChanges();

                //OrderDetail
                foreach (var item in cart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        QuantityBuy = item.QuantityBuy,
                        Price = item.QuantityBuy * item.product.Price,
                        OrderID = order.OrderID,
                        ProductID = item.product.ProductID,
                    };
                    db.orderDetail.Add(orderDetail);
                    db.SaveChanges();

                    //giảm số lượng sp sau khi đặt
                    var product = db.product.FirstOrDefault(x => x.ProductID == item.product.ProductID);
                    product.Quantity = product.Quantity - item.QuantityBuy;
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                cart.Clear();
                Session["cart"] = cart;
            }
            //Nếu khách hàng không có tài khoản thêm vào giỏ hàng
            else
            {
                //Orders
                var order = new Order();
                order.UserID = null;
                order.DateTimeOrder = DateTime.Now;
                order.Status = null;
                order.OrderPhoneNumber = num;
                db.order.Add(order);
                db.SaveChanges();
                //OrderDetail
                foreach (var item1 in cart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        QuantityBuy = item1.QuantityBuy,
                        Price = item1.QuantityBuy * item1.product.Price,
                        OrderID = order.OrderID,
                        ProductID = item1.product.ProductID,
                    };
                    db.orderDetail.Add(orderDetail);
                    db.SaveChanges();

                    //giảm số lượng sp sau khi đặt
                    var product = db.product.FirstOrDefault(x => x.ProductID == item1.product.ProductID);
                    product.Quantity = product.Quantity - item1.QuantityBuy;
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                cart.Clear();
                Session["cart"] = cart;
            }
            return RedirectToAction("Menu", "Home");
        }

        //Update số lượng sản phẩm
        public ActionResult UpdateQuantity(int quantity, int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].QuantityBuy = quantity;
            }
            Session["cart"] = cart;
            return RedirectToAction("Index", "Cart");
        }

        //Kiểm tra sản phẩm đã tồn tại chưa
        private int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].product.ProductID.Equals(id))
                    return i;
            }
            return -1;
        }

    }
}