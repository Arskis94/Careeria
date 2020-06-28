using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            if(Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Not logged in";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = Session["UserName"];
                NorthwindEntities Db = new NorthwindEntities();
                List<Products> Model = Db.Products.ToList();
                Db.Dispose();
                return View(Model);
            }
        }
        public ActionResult Index2()
        {
            NorthwindEntities Db = new NorthwindEntities();
            List<Products> Model = Db.Products.ToList();
            Db.Dispose();
            return View(Model);
        }
    }
}