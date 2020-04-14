using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agriculure.WebUi.Models;
using Agriculure.WebUi.Custom_Classes;
using Agriculure.WebUi.ViewModel;

namespace Agriculure.WebUi.Controllers
{
    public class UsersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Users
        public ActionResult Index()
        {
            User user = (User)Session["currentUser"];
            if (user != null)
            {
                var users = db.Users.Include(u => u.Role);
                return View(users.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "ID", "RoleName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,Email,RoleID,Liecnse,NID,Password,Phone,CompanyName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "ID", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (TempData["EmailExists"] != null)
            {
                ViewBag.EmailNotValid = "Email is not valid";
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,Email,RoleID,Liecnse,NID,Password,Phone,CompanyName")] User user)
        {
            if (db.Users.Any(z => z.Email == user.Email && z.ID != user.ID))
            {
                TempData["EmailExists"] = "EmailNotValid";
                return RedirectToAction("Edit");
            }
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile", "Home", new { user.ID });
            }
            ViewBag.RoleID = new SelectList(db.Roles, "ID", "RoleName", user.RoleID);
            return View(user);
        }

        public ActionResult ChangePassword(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = user.ID;
            return View();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(long? id, string Password, string oldPass, string confirmPass)
        {
            try
            {
                string encOldPass = PasswordEncryptor.Encrypt(oldPass);
                User user = db.Users.Where(z => z.ID == id && z.Password == encOldPass).FirstOrDefault();
                if (user != null)
                {
                    user.Password = PasswordEncryptor.Encrypt(Password);

                    if (Password == confirmPass)
                    {
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Profile", "Home", new { user.ID });
                    }
                    else
                    {
                        ViewBag.NotMatching = "confirm password not matching";
                        ViewBag.Id = id;
                        return View();
                    }

                }
                else
                {
                    ViewBag.WrongOldPass = "Wrong Old Password";
                    ViewBag.Id = id;
                    return View();
                }

            }
            catch (Exception)
            {
                ViewBag.somethingwentwrong = "some thing went wrong";
                ViewBag.Id = id;
                return View();

            }

        }

        // GET: Users/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
