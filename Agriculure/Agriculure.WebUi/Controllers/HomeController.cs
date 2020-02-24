﻿using Agriculure.WebUi.Models;
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
        public ActionResult Login(string Email,string Password) {
            var farmer = _dbContext.Farmers.Where(z => z.FarmerEmail == Email && z.FarmerLiecnse == Password).Any();
            if(farmer == true)
            {
                return RedirectToAction("Hello");
            }
            else
            return RedirectToAction("Index");
        }


    }
}