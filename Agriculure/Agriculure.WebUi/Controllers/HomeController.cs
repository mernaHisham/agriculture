﻿using Agriculure.WebUi.BLLs;
using Agriculure.WebUi.Custom_Classes;
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
        AuthBLL authBLL = new AuthBLL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hello() => View();

        [HttpGet]
        public ActionResult Register()
        {
            //var SupplierLst = _dbContext.FarmSuppliers.ToList();
            //SelectList _SupplierLst = new SelectList(SupplierLst, "ID", "FarmerSName");

            //var stackHolderTypes = StackHolderType.GetStackHolderTypes();
            var roles = _dbContext.Roles.Select(x => new RoleVM
            {
                RoleID = x.RoleID,
                RoleName = x.RoleName
            }).ToList();

            //var s = new List<StackHolderTypeModel>
            //{
            //    new StackHolderTypeModel{ StackHolderTypeId = 1, StackHolderTypeName = "F"},
            //    new StackHolderTypeModel{ StackHolderTypeId = 2, StackHolderTypeName = "L"},
            //    new StackHolderTypeModel{ StackHolderTypeId = 3, StackHolderTypeName = "D"},
            //    new StackHolderTypeModel{ StackHolderTypeId = 4, StackHolderTypeName = "S"},
            //};
            SelectList _stackHolderTypes = new SelectList(roles, "RoleID", "RoleName");


            ViewBag.FarmerSID = _stackHolderTypes;

            UserVM userVM = new UserVM();

            //FarmerVM objVM = new FarmerVM();
            return View(userVM);
        }

        [HttpPost]
        public ActionResult Register(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var result = authBLL.RegisterUser(userVM);

                if(result > 0)
                    return RedirectToAction("Index");
            }
            
            var roles = _dbContext.Roles.Select(x => new RoleVM
            {
                RoleID = x.RoleID,
                RoleName = x.RoleName
            }).ToList();
            SelectList _stackHolderTypes = new SelectList(roles, "RoleID", "RoleName");
            
            ViewBag.FarmerSID = _stackHolderTypes;
            //var roles = _dbContext.Roles.ToList();
            //SelectList _stackHolderTypes = new SelectList(roles, "ID", "RoleName");


            //ViewBag.FarmerSID = _stackHolderTypes;

            return View(userVM);
        }

        [HttpPost]
        public ActionResult PostFarmer([Bind(Include = "ID,FarmerName,FarmerAddress,FarmerEmail,FarmerRole,FarmerLiecnse,FarmerNID,FarmerSID")] FarmerVM objVM)
        {
            //Farmer _Farmer = new Farmer()
            //{
            //    FarmerName = objVM.FarmerName,
            //    FarmerEmail = objVM.FarmerEmail,
            //    FarmerAddress = objVM.FarmerAddress,
            //    FarmerLiecnse = objVM.FarmerLiecnse,
            //    FarmerNID = objVM.FarmerNID,
            //    FarmerRole = objVM.FarmerRole,
            //    FarmerSID = objVM.FarmerSID
            //};
            //_dbContext.Farmers.Add(_Farmer);
            //_dbContext.SaveChanges();
            return View("Index");
        }
        [HttpPost]
        public ActionResult Login(string Email,string Password) {
            var farmer = /*_dbContext.Farmers.Where(z => z.FarmerEmail == Email && z.FarmerLiecnse == Password).Any()*/ true ;
            if(farmer == true)
            {
                return RedirectToAction("Hello");
            }
            else
            return RedirectToAction("Index");
        }

        [Route("Home/Profile/{UserId}")]
        [HttpGet]
        public ActionResult Profile(long UserId)
        {
            var user = _dbContext.Users.Where(x => x.ID == UserId).FirstOrDefault();
            return View(user);
        }

    }
}