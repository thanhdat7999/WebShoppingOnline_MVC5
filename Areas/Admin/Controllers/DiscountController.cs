using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMDT_Web.Data;
using TMDT_Web.Models.Domain;

namespace TMDT_Web.Areas.Admin.Controllers
{
    public class DiscountController : Controller
    {
        DataContext db=new DataContext();
        // GET: Admin/Discount
        public ActionResult Index()
        {
            ViewData["discount"] = db.discounts.ToList();
            return View();
        }

        //Tạo sự kiện gồm tên, số lượng mã, phần trăm khuyến mãi
        [HttpPost]
        public ActionResult Create([Bind(Include = "EventID, Event, DiscountPercent, Quantity, Status, Content")]Discount discount, HttpPostedFileBase Image )
        {
            if (Image == null)
            {
                discount.Image = "abc.jpg";
            }
            else
            {
                string _fileName = Path.GetFileName(Image.FileName);
                discount.Image = _fileName;
                string _path = Path.Combine(Server.MapPath("~/Images/Imgs/"), _fileName);
                Image.SaveAs(_path);
            }

            string randomCode = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var phrase = new char[10];
            for (int i = 0; i < phrase.Length; i++)
            {
                phrase[i] = randomCode[random.Next(randomCode.Length)];
            }
            discount.CodeRnd = new string(phrase);
            db.discounts.Add(discount);
            db.SaveChanges();
            return RedirectToAction("index","Discount");
        }
        public ActionResult Delete(int id)
        {
            var discount = db.discounts.Find(id);
            db.discounts.Remove(discount);
            db.SaveChanges();
            return RedirectToAction("Index", "Discount");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var discount = db.discounts.Find(id);
            return View(discount);
        }
        [HttpPost]
        public ActionResult Edit(/*int id, string EName, int EPer, int QuantCode*/ Discount discount, HttpPostedFileBase EImage, string Image)
        {
            if (EImage == null)
            {
                discount.Image = Image;
            }
            else
            {
                string _fileName = Path.GetFileName(EImage.FileName);
                discount.Image = _fileName;
                string _path = Path.Combine(Server.MapPath("~/Images/Imgs/"), _fileName);
                EImage.SaveAs(_path);
            }
            db.Entry(discount).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Discount");
        }
    }
}