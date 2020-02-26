using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agriculure.WebUi.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [Route("Register")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}