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


            var s = new List<StackHolderTypeModel>
            {
                new StackHolderTypeModel{ StackHolderTypeId = 0, StackHolderTypeName = "F"},
                new StackHolderTypeModel{ StackHolderTypeId = 0, StackHolderTypeName = "L"},
                new StackHolderTypeModel{ StackHolderTypeId = 0, StackHolderTypeName = "D"},
                new StackHolderTypeModel{ StackHolderTypeId = 0, StackHolderTypeName = "S"},
            };
            SelectList _stackHolderTypes = new SelectList(s, "StackHolderTypeId", "StackHolderTypeName");


            ViewBag.FarmerSID = _stackHolderTypes;

            RegisterationObject objVM = new RegisterationObject();

            //FarmerVM objVM = new FarmerVM();
            return View(objVM);
        }

        [HttpPost]
        public ActionResult Register(RegisterationObject registerationObject)
        {

            return RedirectToAction("Index");
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