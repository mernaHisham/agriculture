using Agriculure.WebUi.BLLs;
using Agriculure.WebUi.Custom_Classes;
using Agriculure.WebUi.Models;
using Agriculure.WebUi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email,string Password)
        {
            if (ModelState.IsValid)
            {
                var encryptedPass = PasswordEncryptor.Encrypt(Password);
                var user = _dbContext.Users.Where(x => x.Email == Email && x.Password == encryptedPass).FirstOrDefault();
                if (user != null)
                {
                    Session["currentUser"] = user;
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.ErrorMsg = "Email or Password is Incorrect";
            return View();
            
        }

        [Route("Home/Profile/{UserId}")]
        [HttpGet]
        public ActionResult Profile(long UserId)
        {
            var user = _dbContext.Users.Where(x => x.ID == UserId).FirstOrDefault();
            var decryptedPass = PasswordEncryptor.Decrypt(user.Password);
            ViewBag.pass = decryptedPass;
            return View(user);
        }
        public ActionResult Logout()
        {
            Session["currentUser"] = null;
            return RedirectToAction("Index");
        }
        
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            var user = _dbContext.Users.Where(x => x.Email == Email).FirstOrDefault();
            if(user != null)
            {
                int result = SendForgetPasswordMail(user.Email);
                if(result == 0)
                {
                    user.Password = "1456";
                    _dbContext.SaveChanges();
                    return RedirectToAction("EmailSent");
                }

                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult EmailSent()
        {
            return View();
        }

        public int SendForgetPasswordMail(string ToEmail)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("dc-b34e10b12d8e.rpaegypt.com", 587);
                MailMessage message = new MailMessage();
                message.From = new MailAddress("alisakralaraby@gmail.com");
                message.Body = "Use This Password Temporarly, we recommend you to change it. /n 1456";
                message.To.Add(ToEmail);

                message.Subject = "Temporary Password";
                smtpClient.UseDefaultCredentials = true;
                smtpClient.EnableSsl = false;

                //message.Attachments.Add(new Attachment(filePath));


                smtpClient.Credentials = new System.Net.NetworkCredential("bookup@rpaegypt.com", "P@ssw0rd");
                smtpClient.Timeout = 15 * 60 * 1000;
                smtpClient.Send(message);

                message = null;
                return 0;
            }
            catch (Exception x)
            {
                return -1;
            }
        }
    }
}