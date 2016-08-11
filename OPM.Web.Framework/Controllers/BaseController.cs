using OPM.Core.IocReg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OPM.Web.Framework.Controllers
{
    public class BaseController : Controller
    {
        public  OPMContext OPMContext
        {
            get
            {
                if (Singleton<OPMContext>.Instance == null)
                {
                    Singleton<OPMContext>.Instance = new OPMContext();
                }
                return Singleton<OPMContext>.Instance;
            }
        }

    }
}