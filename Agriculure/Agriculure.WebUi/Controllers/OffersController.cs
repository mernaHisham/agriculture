using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agriculure.WebUi.Models;

namespace Agriculure.WebUi.Controllers
{
    public class OffersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Offers
        public ActionResult Index()
        {
            var offers = db.Offers.Include(o => o.Product).Include(o => o.User);
            return View(offers.ToList());
        }

        // GET: Offers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // GET: Offers/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name");
            ViewBag.offerowner = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID,StartDate,EndDate,Status,offerowner,Descreption,unit,quntity,price,currency")] Offer offer)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", offer.ProductID);
            ViewBag.offerowner = new SelectList(db.Users, "ID", "Name", offer.offerowner);
            return View(offer);
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", offer.ProductID);
            ViewBag.offerowner = new SelectList(db.Users, "ID", "Name", offer.offerowner);
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductID,StartDate,EndDate,Status,offerowner,Descreption,unit,quntity,price,currency")] Offer offer)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", offer.ProductID);
            ViewBag.offerowner = new SelectList(db.Users, "ID", "Name", offer.offerowner);
            return View(offer);
        }

        // GET: Offers/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Offer offer = db.Offers.Find(id);
        //    if (offer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(offer);
        //}

        // POST: Offers/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Offer offer = db.Offers.Find(id);
            db.Offers.Remove(offer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
