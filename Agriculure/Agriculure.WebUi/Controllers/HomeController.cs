using Agriculure.WebUi.Models;
using Agriculure.WebUi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agriculure.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private Model1 _dbContext = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hello() => View();
        public ActionResult Register()
        {
            var SupplierLst = _dbContext.FarmSuppliers.ToList();
            SelectList _SupplierLst = new SelectList(SupplierLst, "ID", "FarmerSName");
            ViewBag.FarmerSID = _SupplierLst;
            FarmerVM objVM = new FarmerVM();
            return View(objVM);
        }
        [HttpPost]
        public ActionResult PostFarmer([Bind(Include = "ID,FarmerName,FarmerAddress,FarmerEmail,FarmerRole,FarmerLiecnse,FarmerNID,FarmerSID")] FarmerVM objVM)
        {
            Farmer _Farmer = new Farmer()
            {
                FarmerName = objVM.FarmerName,
                FarmerEmail = objVM.FarmerEmail,
                FarmerAddress = objVM.FarmerAddress,
                FarmerLiecnse = objVM.FarmerLiecnse,
                FarmerNID = objVM.FarmerNID,
                FarmerRole = objVM.FarmerRole,
                FarmerSID = objVM.FarmerSID
            };
            _dbContext.Farmers.Add(_Farmer);
            _dbContext.SaveChanges();
            return View("Index");
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password) {
            var farmer = _dbContext.Farmers.Where(z => z.FarmerEmail == Email && z.FarmerPassword == Password).Any();
            var farmerSupplier = _dbContext.FarmSuppliers.Where(z => z.FarmerSEmail == Email && z.FarmerSPassword == Password).Any();
            var producer = _dbContext.Producers.Where(z => z.ProducerEmail == Email && z.ProducerPassword == Password).Any();
            var retailer = _dbContext.Retailers.Where(z => z.RetailerEmail == Email && z.RetailerPassword == Password).Any();
            var logistic = _dbContext.Logistics.Where(z => z.LogisticsEmail == Email && z.LogisticPassword == Password).Any();
            if (farmer == true || farmerSupplier == true || producer == true || retailer == true|| logistic == true)
            {
                if(farmer == true) Session["currentUser"] = _dbContext.Farmers.Where(z => z.FarmerEmail == Email && z.FarmerPassword == Password).FirstOrDefault();
                if(farmerSupplier == true) Session["currentUser"] = _dbContext.FarmSuppliers.Where(z => z.FarmerSEmail == Email && z.FarmerSPassword == Password).FirstOrDefault();
                if (producer == true) Session["currentUser"] = _dbContext.Producers.Where(z => z.ProducerEmail == Email && z.ProducerPassword == Password).FirstOrDefault();
                if (retailer == true) Session["currentUser"] = _dbContext.Retailers.Where(z => z.RetailerEmail == Email && z.RetailerPassword == Password).FirstOrDefault();
                if (logistic == true) Session["currentUser"] = _dbContext.Logistics.Where(z => z.LogisticsEmail == Email && z.LogisticPassword == Password).FirstOrDefault();
                return RedirectToAction("Hello");
            }
            else
            return RedirectToAction("Index");
        }


    }
}