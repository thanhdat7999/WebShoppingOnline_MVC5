using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;

namespace TMDT_Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.order.ToList();
            return View(orders);
        }
        public ActionResult Accept(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var accept = db.order.FirstOrDefault(x => x.OrderID == id);
                accept.Status = 1;
                db.Entry(accept).State = EntityState.Modified;
                db.SaveChanges();
                //Tính tích điểm cho ng dùng
                int sum = 0;
                var prices = db.orderDetail.Where(x => x.OrderID == id).ToList();
                for (int i = 0; i < prices.Count; i++)
                {
                    sum += prices[i].Price;
                }
                var total = sum;
                if (total >= 1000000)
                {
                    var points = db.account.FirstOrDefault(x => x.UserID == accept.Account.UserID);
                    if (points.Points == null)
                    {
                        points.Points = 0;
                    }
                    points.Points = points.Points + 1;
                    db.Entry(points).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                var accept = db.order.FirstOrDefault(x => x.OrderPhoneNumber == id);
                accept.Status = 1;
                db.Entry(accept).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("index", "order");
        }
        public ActionResult Transport(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderID == id);
                cancel.Status = 3;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderPhoneNumber == id);
                cancel.Status = 3;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("index", "order");
        }
        public ActionResult Finish(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderID == id);
                cancel.Status = 4;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderPhoneNumber == id);
                cancel.Status = 4;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("index", "order");
        }
        public ActionResult Cancel(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderID == id);
                cancel.Status = 2;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                var cancel = db.order.FirstOrDefault(x => x.OrderPhoneNumber == id);
                cancel.Status = 2;
                db.Entry(cancel).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("index", "order");
        }
        public ActionResult Detail(int id)
        {
            var detail = db.orderDetail.Where(x => x.OrderID == id).ToList();
            return View(detail);
        }
    }
}