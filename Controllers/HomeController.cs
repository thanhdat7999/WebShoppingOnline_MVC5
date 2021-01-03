using PagedList;
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
    public class HomeController : Controller
    {
        //Dữ liệu
        DataContext db = new DataContext();
        //Giao diện trang chủ
        public ActionResult Index()
        {
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }

            //Lấy 3 sản phẩm ngẫu nhiên
            var random = db.product.Where(x=>x.Quantity > 0).Take(6).ToList();
            ViewData["random"] = random;

            //Lấy 3 sản phẩm có số người xem nhiều
            var viewCount = db.product.Where(x => x.Quantity > 0).Take(6).OrderByDescending(x => x.View).ToList();
            ViewData["viewCount"] = viewCount;

            //Lấy 3 sản phẩm mới nhất
            var viewLatest = db.product.Where(x => x.Quantity > 0).Take(6).OrderByDescending(x => x.Date).ToList();
            ViewData["viewLatest"] = viewLatest;

            //Chat
            var user = db.account.FirstOrDefault(x => x.UserName == User.Identity.Name);

            //Test
            /*var query1 = from x in db.orderDetail select x.Product.ProductName;
            var query2 = from y in db.orderDetail
                         group y by y.Product.ProductName into g
                         select ;*/
            /*var item = db.orderDetail.GroupBy(x => x.ProductID).Select(group => new { ProductID = group.Key, OrderDetail = group.ToList() }).ToList();*/
           
          /*  ViewBag.item = item;

            var test = db.orderDetail.Select(x=>x.Product.ProductName).ToList();
            ViewBag.test = test;*/
            return View(user);
        }
        public class test
        {
            public string Name { get; set; }
            public int age { get; set; }
        }
        //Giao diện danh sách sản phẩm
        public ActionResult Menu(int? page, int? type)
        {
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }
            //Side bar cty laptop
            ViewBag.SideBar = db.company.ToList();
            //Side bar hãng laptop
            ViewBag.SideBarBrand = db.brand.ToList();

            //Page list
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            if(type==1 || type == null)
            {
                //Danh sách sản phẩm
                ViewData["Menu"] = db.product.Where(x => x.Quantity > 0).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.type = 1;
            }
            else if(type==2)
            {
                //Danh sách sản phẩm
                ViewData["Menu"] = db.product.Where(x => x.Quantity > 0).OrderByDescending(x=>x.View).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.type = 2;
            }
            else if(type==3)
            {
                //Danh sách sản phẩm
                ViewData["Menu"] = db.product.Where(x => x.Quantity > 0).OrderByDescending(x => x.Date).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.type = 3;
            }
            return View();
        }

        //Trang tìm kiếm
        public ActionResult Search(string search, int? page)
        {
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }
            //Side bar cty laptop
            ViewBag.SideBar = db.company.ToList();
            //Side bar hãng laptop
            ViewBag.SideBarBrand = db.brand.ToList();
            

            //Page list
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            //Danh sách sản phẩm khi tìm kiếm
            ViewData["Search"] = db.product.Where(x => x.ProductName == search).ToList().ToPagedList(pageNumber, pageSize);
            return View();
        }
        //Trang sidebar compnanies
        public ActionResult CompanySidebar(int companysidebar, int? page)
        {
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }
            //Side bar cty laptop
            ViewBag.SideBar = db.company.ToList();
            //Side bar hãng laptop
            ViewBag.SideBarBrand = db.brand.ToList();

            //Page list
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            //Danh sách sản phẩm khi tìm kiếm
            ViewData["CompanySidebar"] = db.product.Where(x => x.Brand.CompanyID == companysidebar).ToList().ToPagedList(pageNumber, pageSize);
            return View();
        }

        //Trang sidebar brand
        public ActionResult BrandSidebar(int brandsidebar, int? page)
        {
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }
            //Side bar cty laptop
            ViewBag.SideBar = db.company.ToList();
            //Side bar hãng laptop
            ViewBag.SideBarBrand = db.brand.ToList();

            //Page list
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            //Danh sách sản phẩm khi tìm kiếm
            ViewData["BrandSidebar"] = db.product.Where(x => x.BrandID == brandsidebar).ToList().ToPagedList(pageNumber, pageSize);
            return View();
        }

        //Trang chi tiết sản phẩm
        public ActionResult Detail(int? id, int? page, int? star, string error)
        {
            ViewBag.error = error;

            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }
            Product product = db.product.FirstOrDefault(x => x.ProductID == id);

            //Đánh giá sao cho sản phẩm 


            //Side bar cty laptop
            ViewBag.SideBar = db.company.ToList();
            //Side bar hãng laptop
            ViewBag.SideBarBrand = db.brand.ToList();

            //Page list
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            //Sản phẩm cùng loại
            var products = db.product.Where(x => x.Brand.CompanyID == product.Brand.CompanyID && x.ProductID != id && x.Quantity > 0).ToList().ToPagedList(pageNumber,pageSize);
            //Đếm view và lưu vào csdl
            var viewCount = db.product.FirstOrDefault(x => x.ProductID == id);
            if(viewCount.View == null)
            {
                viewCount.View = 0;
                viewCount.View += 1;
            }
            else
            {
                viewCount.View += 1;
            }
            db.Entry(viewCount).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            ViewData["products"] = products;
            

            //Hiển thị reviews
            var review = db.review.Where(x => x.ProductID == id).ToList();
            var countReview = db.review.Where(x => x.ProductID == id).Count();
            ViewBag.countReview = countReview;
            ViewBag.review = review;

            return View(product);
        }
        //Chức năng cmt
        public ActionResult Review([Bind(Include = "ID, Comment, UserID, ProductID")] Review review, int proid)
        {
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }

            try
            {
                var user = db.account.FirstOrDefault(x => x.UserName == User.Identity.Name);
                review.UserID = user.UserID;
                review.ProductID = proid;
                review.DateTime = DateTime.Now;
                db.review.Add(review);
                db.SaveChanges();
                return RedirectToAction("Detail", "Home", new { id = proid });
            }
            catch
            {
                return RedirectToAction("Detail", "Home", new { id = proid, error = "You have to login to comment this post" });
            }
        }
        public ActionResult ChatRoom()
        {
            //Đếm sản phẩm trong giỏ hàng
            List<Item> cart = (List<Item>)Session["cart"];
            if (Session["cart"] == null)
            {
                ViewData["countCartProducts"] = 0;
            }
            else
            {
                ViewData["countCartProducts"] = cart.Count;
            }

            //Chat
            var user = db.account.FirstOrDefault(x => x.UserName == User.Identity.Name);
            return View(user);
        }
        //Trang event
        public ActionResult Event()
        {
            return View(db.discounts.Where(x=>x.Status=="Publish").ToList());
        }
    }
}