using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TMDT_Web.Data;
using TMDT_Web.Models;
using TMDT_Web.Models.Domain;

namespace TMDT_Web.Controllers
{
    public class AuthenticationController : Controller
    {
        DataContext db = new DataContext();
        // GET: Authentication
        //Giao diện trang người dùng
        public ActionResult Index()
        {
            return View(db.account.FirstOrDefault(x => x.UserName == User.Identity.Name));
        }

        //Chức năng và giao diện đăng nhập
        public ActionResult Login()
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
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin([Bind(Include = "Email, Password")] Account user)
        {
            var accounts = db.account.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
           
            if (user.Email == accounts.Email && user.Password == accounts.Password)
            {
                user.UserName = accounts.UserName;
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("index", "Home");
            }
            else
            {
                ModelState.AddModelError("Lỗi 1", "Lỗi 2");
                return View("Login");
            }
        }

        //Chức năng và giao diện đăng nhập
        public ActionResult Register()
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
            return View();
        }
        [HttpPost]
        public ActionResult DoRegister([Bind(Include = "FirstName, LastName, UserName, PhoneNumber, Address, Age, Email, Password")] Account register)
        {
            var check = db.account.Count();
           
            Account account = new Account();
            account.FirstName = register.FirstName;
            account.LastName = register.LastName;
            account.UserName = register.UserName;
            account.PhoneNumber = register.PhoneNumber;
            account.Address = register.Address;
            account.Age = register.Age;
            account.Email = register.Email;
            account.Password = register.Password;
            account.Avatar = "user.png";   

            if (check == 0)
            {
                account.RoleID = 1;
            }
            else
            {
                account.RoleID = 2;
            }

            db.account.Add(account);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        //Chức năng đăng xuất

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        //Chức năng đổi mật khẩu
        public ActionResult ChangePassword(int id, string cpw, string npw, string cnpw)
        {
            var acc = db.account.FirstOrDefault(x => x.UserID == id);
            if (cpw == acc.Password && npw == cnpw)
            {
                acc.Password = npw;
                db.Entry(acc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        //Chức năng ảnh giao diện
        public ActionResult ChangeAvatar(HttpPostedFileBase Image)
        {
            var account = db.account.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (Image.ContentLength == 0)
            {
                account.Avatar = "user.png";
            }
            string _fileName = Path.GetFileName(Image.FileName);
            account.Avatar = _fileName;
            string _path = Path.Combine(Server.MapPath("~/Images/Avatar/"), _fileName);
            Image.SaveAs(_path);
            db.Entry(account).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Authentication");
        }
    }
}