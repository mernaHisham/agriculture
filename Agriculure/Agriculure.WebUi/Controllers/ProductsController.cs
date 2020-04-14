using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agriculure.WebUi.Custom_Classes;
using Agriculure.WebUi.Models;

namespace Agriculure.WebUi.Controllers
{
    public class ProductsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Products
        public ActionResult Index()
        {
            if (TempData["RelatedDataError"] != null)
            {
                ViewBag.RelatedDataError = "Can`t delete offer bacause of related offers";
            }
            User user = (User)Session["currentUser"];
            if (user != null)
            {
                var products = db.Products.Where(x => x.UserID != user.ID);
                return View(products.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ReportProducts()
        {
            User user = (User)Session["currentUser"];
            if (user != null)
            {
                var products = db.Products;
                return View(products.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult UserProducts()
        {
            User user = (User)Session["currentUser"];
            var products = db.Products.Where(x => x.User.ID == user.ID);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Liecnse,UserID,Description,image")] Product product, HttpPostedFileBase imageFile)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                product.UserID = (Session["currentUser"] as User).ID;
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/imgs"),
                    Path.GetFileName(imageFile.FileName));
                    imageFile.SaveAs(path);
                    product.image = imageFile.FileName;
                    product.CreationDate = DateTime.Today;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", product.UserID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", product.UserID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Liecnse,UserID,Description,image")] Product product, HttpPostedFileBase imageFile)
        {
            if (Session["currentUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/imgs"),
                    Path.GetFileName(imageFile.FileName));
                    imageFile.SaveAs(path);
                    product.image = imageFile.FileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", product.UserID);
            return View(product);
        }

        public ActionResult Delete(long id)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["RelatedDataError"] = "RelatedDataError";
                return RedirectToAction("Index");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult ProductHistory(int Id = 0)
        {
            var user = (User)Session["currentUser"];
            if (user == null)
                return RedirectToAction("Login", "Home");

            ViewBag.prodId = Id;

            return View();
        }

        [HttpGet]
        public ActionResult ProdDetails(long ID)
        {
            var user = (User)Session["currentUser"];
            var product = db.Products.Where(x => x.ID == ID && x.UserID == user.ID).FirstOrDefault();
            if (product == null)
                return PartialView(null);

            return PartialView(product);
        }


        [HttpGet]
        public ActionResult ProdHistory(long ID)
        {
            var user = (User)Session["currentUser"];
            var product = db.Products.Where(x => x.ID == ID && x.UserID == user.ID).FirstOrDefault();
            if (product == null)
                return PartialView(null);

            List<ICollection<Contract>> prodContracts = new List<ICollection<Contract>>();
            List<ProductDetails> productDetails = new List<ProductDetails>();

            if (product != null)
            {
                var prodOffers = db.Offers.Where(x => x.ProductID == product.ID).ToList();
                foreach (var item in prodOffers)
                {
                    prodContracts.Add(item.Contracts);
                    productDetails.Add(new ProductDetails
                    {
                        Description = item.Descreption,
                        ActionDate = item.StartDate,
                        UserName1 = item.User.Name,
                        ActionType = "Offer On This Product"
                    });
                }
                foreach (var item in prodContracts)
                {
                    foreach (var res in item)
                    {
                        productDetails.Add(new ProductDetails
                        {
                            ActionDate = Convert.ToDateTime(res.acceptDate),
                            ActionType = "Contract On This Contract",
                            UserName1 = res.User1.Name,
                            UserName2 = res.User.Name
                        });
                    }
                }

                productDetails.Add(new ProductDetails
                {
                    Description = product.Description,
                    ActionType = "Product Created",
                    UserName1 = product.User.Name,
                    ActionDate = product.CreationDate
                });
                productDetails.OrderByDescending(x => x.ActionDate);
            }

            return PartialView(productDetails);
        }


        public ActionResult gggggg(long ID)
        {
            var data = new { status = "ok", result = "Ok" };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProductHistory(long ID)
        {
            var user = (User)Session["currentUser"];
            var product = db.Products.Where(x => x.ID == ID && x.UserID == user.ID).FirstOrDefault();
            if (product != null)
            {

            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult GetByName(DateTime? date = null, string name = "")
        {
            User user = (User)Session["currentUser"];
            var products = db.Products.Where(x => x.UserID != user.ID).ToList();

            if (name != null && name != "")
            {
                var result = products.Where(x => x.Name.ToLower().StartsWith(name.ToLower())).ToList().Distinct();
                return PartialView(result);
            }
            else if (date != null)
            {
                var dd = products.Where(x => x.CreationDate == date).ToList();
                var result = products.Where(x => x.CreationDate == date).ToList().Distinct();
                return PartialView(result);
            }
            else
            {
                return PartialView(products);
            }
        }
    }
}
