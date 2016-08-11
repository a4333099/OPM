using OPM.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OPM.Web.Controllers
{
    public class HomeController: BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}