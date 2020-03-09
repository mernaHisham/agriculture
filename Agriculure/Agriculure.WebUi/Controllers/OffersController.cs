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
            User user = (User)Session["currentUser"];
            var userOffersFromContracts = db.Contracts.Where(x => x.buyerID == user.ID || x.sellerID == user.ID).Select(x => x.OfferID).ToList();

            var offers = db.Offers.Where(x => x.User.ID != user.ID && !userOffersFromContracts.Any(r => x.ID == r)).ToList();
            return View(offers);
        }
        public ActionResult UserOffers()
        {
            User user = (User)Session["currentUser"];
            
            var Offers = db.Offers.Where(x => x.User.ID == user.ID);
            return View(Offers.ToList());
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
            var loggedUser = (User)Session["currentUser"];
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name");
            ViewBag.offerowner = loggedUser.ID;
            ViewBag.Name = loggedUser.Name;

            var userRoleId = loggedUser.RoleID;
            ViewBag.userRoleId = userRoleId;

            ViewBag.offerowner = new SelectList(db.Users, "ID", "Name", loggedUser.ID);
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID,StartDate,EndDate,Status,offerowner,Descreption,unit,quntity,price,currency")] Offer offer)
        {
            var user = (User) Session["currentUser"];
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            offer.offerowner = user.ID;
            db.Offers.Add(offer);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                
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

        [HttpPost]
        public ActionResult RequestContract(long id)
        {
            Offer offer = db.Offers.Find(id);

            User user = (User)Session["currentUser"];

            var contract = new Contract
            {
                OfferID = offer.ID,
                Price = Convert.ToDouble(offer.price),
                Quantity = Convert.ToInt32(offer.quntity),
                requestDate = DateTime.Now,
                buyerID = user.ID,
                sellerID = offer.User.ID,
                status = null,
                acceptDate = null
            };

            db.Contracts.Add(contract);
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
