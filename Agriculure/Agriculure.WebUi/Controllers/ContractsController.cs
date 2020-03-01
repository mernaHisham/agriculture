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
    public class ContractsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Contracts
        public ActionResult Index()
        {
            User user = (User)Session["currentUser"];
            //shows pending contracts (that waits for acceptance or rejection) as a seller
            var contracts = db.Contracts.Where(x => x.status == null && x.sellerID == user.ID).ToList();
            return View(contracts);
        }

        public ActionResult UserContracts()
        {
            User user = (User)Session["currentUser"];
            //shows contracts (that are accepted) as a BUYER
            var AcceptedBUYERcontracts = db.Contracts.Where(x => x.buyerID == user.ID && x.status == true);

            //shows pending contracts (that waits for acceptance or rejection) as a BUYER
            var PendingBuyercontracts = db.Contracts.Where(x => x.buyerID == user.ID && x.status == null).ToList();

            //shows contracts (that are rejected) as a BUYER
            var rejectedBUYERContracts = db.Contracts.Where(x => x.buyerID == user.ID && x.status == false).ToList();

            //shows contracts (that are accepted) as a BUYER
            var AcceptedSellerContracts = db.Contracts.Where(x => x.sellerID == user.ID && x.status == true).ToList();

            ViewBag.PendingBuyercontracts = PendingBuyercontracts;
            ViewBag.rejectedBUYERContracts = rejectedBUYERContracts;
            ViewBag.AcceptedSellerContracts = AcceptedSellerContracts;

            return View(AcceptedBUYERcontracts.ToList());
        }

        // GET: Contracts/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Contracts/Create
        public ActionResult Create()
        {
            ViewBag.OfferID = new SelectList(db.Offers, "ID", "Descreption");
            ViewBag.sellerID = new SelectList(db.Users, "ID", "Name");
            ViewBag.buyerID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OfferID,Quantity,Price,buyerID,sellerID,requestDate,status,acceptDate")] Contract contract)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OfferID = new SelectList(db.Offers, "ID", "Descreption", contract.OfferID);
            ViewBag.sellerID = new SelectList(db.Users, "ID", "Name", contract.sellerID);
            ViewBag.buyerID = new SelectList(db.Users, "ID", "Name", contract.buyerID);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfferID = new SelectList(db.Offers, "ID", "Descreption", contract.OfferID);
            ViewBag.sellerID = new SelectList(db.Users, "ID", "Name", contract.sellerID);
            ViewBag.buyerID = new SelectList(db.Users, "ID", "Name", contract.buyerID);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OfferID,Quantity,Price,buyerID,sellerID,requestDate,status,acceptDate")] Contract contract)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OfferID = new SelectList(db.Offers, "ID", "Descreption", contract.OfferID);
            ViewBag.sellerID = new SelectList(db.Users, "ID", "Name", contract.sellerID);
            ViewBag.buyerID = new SelectList(db.Users, "ID", "Name", contract.buyerID);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
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

        public ActionResult Accept(long id)
        {
            var contract = db.Contracts.Where(x => x.ID == id).FirstOrDefault();
            if (contract != null)
            {
                contract.status = true;
                contract.acceptDate = DateTime.Now;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
        public ActionResult Reject(long id)
        {
            var contract = db.Contracts.Where(x => x.ID == id).FirstOrDefault();
            if (contract != null)
                contract.status = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
