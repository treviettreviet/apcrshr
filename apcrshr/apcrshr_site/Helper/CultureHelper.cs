using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace apcrshr_site.Helper
{
    public class CultureHelper
    {
        protected HttpSessionState session;

        //constructor   
        public CultureHelper(HttpSessionState httpSessionState)
        {
            session = httpSessionState;
        }
        // Properties  
        public static string CurrentCulture
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    return "VN";
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    return "EN";
                }
                else
                {
                    return "EN";
                }
            }
            set
            {

                if (value == "VN")
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
                }
                else if (value == "EN")
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
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