using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace Agriculure.WebUi.Custom_Classes
{
    public class CultureHelper
    {
        private HttpSessionState _httpSessionState;

        public CultureHelper(HttpSessionState httpSessionState)
        {
            _httpSessionState = httpSessionState;
        }

        public static int CurrentCulture
        {
            get
            {
                if(Thread.CurrentThread.CurrentUICulture.Name == "ar")
                {
                    return 0;
                }else if(Thread.CurrentThread.CurrentUICulture.Name == "en")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if(value == 0)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar");
                }
                else if (value == 1)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                }
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            }
        }

    }
}