using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Not logged in";
            }
            else ViewBag.LoggedStatus = Session["UserName"];
            return View();
        }

        public ActionResult About()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = Session["UserName"];
                ViewBag.Message = "Your application description page.";
                return View();
            }
        }

        public ActionResult Contact()
        {
            if (Session["UserName"] == null) ViewBag.LoggedStatus = "Not logged in";
            else ViewBag.LoggedStatus = Session["UserName"];
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Map()
        {
            if (Session["UserName"] == null) ViewBag.LoggedStatus = "Not logged in";
            else ViewBag.LoggedStatus = Session["UserName"];
            ViewBag.Message = "Map";
            return View();
        }

        public ActionResult Login()
        {
            if (Session["UserName"] == null) ViewBag.LoggedStatus = "Not logged in";
            else ViewBag.LoggedStatus = Session["UserName"];
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Not logged in";
                NorthwindEntities Db = new NorthwindEntities();
                var LoggedUser = Db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
                if (LoggedUser != null)
                {
                    ViewBag.LoginMessage = "Successfull login";
                    ViewBag.LoggedStatus = "Logged in as " + LoginModel.UserName;
                    Session["UserName"] = LoggedUser.UserName;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginMessage = "Login unsuccessfull";
                    ViewBag.LoggedStatus = "Out";
                    LoginModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Login", LoginModel);
                }
            }
            else
            {
              
                Response.Write("<script>alert('You are already logged in!')</script>");
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Not logged in";
            return RedirectToAction("Index", "Home");
        }
    }
}