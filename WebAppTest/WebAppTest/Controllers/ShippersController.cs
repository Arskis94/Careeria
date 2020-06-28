using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class ShippersController : Controller
    {
        // GET: Shippers
        NorthwindEntities Db = new NorthwindEntities();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Not logged in";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = Session["UserName"];
                var shippers = Db.Shippers.Include(s => s.Region);
                return View(shippers.ToList());
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Shippers shippers = Db.Shippers.Find(id);
            if (shippers == null) return HttpNotFound();
            ViewBag.RegionID = new SelectList(Db.Region, "RegionID", "RegionDescription", shippers.RegionID);
            return View(shippers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipperID, CompanyName, Phone, RegionID")] Shippers shippers)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(shippers).State = EntityState.Modified;
                Db.SaveChanges();
                ViewBag.RegionID = new SelectList(Db.Region, "RegionID", "RegionDescription", shippers.RegionID);
                return RedirectToAction("Index");

            }
            return View(shippers);
        }

        public ActionResult Create()
        {
            ViewBag.RegionID = new SelectList(Db.Region, "RegionID", "RegionDescription");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipperID, CompanyName, Phone, RegionID")] Shippers shipper)
        {
            if (ModelState.IsValid)
            {
                Db.Shippers.Add(shipper);
                Db.SaveChanges();
                ViewBag.RegionID = new SelectList(Db.Region, "RegionID", "RegionDescription");
                return RedirectToAction("Index");
            }
            return View(shipper);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Shippers shippers = Db.Shippers.Find(id);
            if (shippers == null) return HttpNotFound();
            return View(shippers);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shippers shippers = Db.Shippers.Find(id);
            Db.Shippers.Remove(shippers);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}