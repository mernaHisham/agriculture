using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Agriculure.WebUi.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Change(string LanguageAbbreviaton)
        {
            if(LanguageAbbreviaton != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbreviaton);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbreviaton);
            }

            HttpCookie httpCookie = new HttpCookie("Language");
            httpCookie.Value = LanguageAbbreviaton;
            Response.Cookies.Add(httpCookie);
            return RedirectToAction("Index", "Home");
        }

    }
}