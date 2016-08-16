using OPM.Core.IocReg;
using OPM.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OPM.Web.Framework.ViewPages
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public OPMContext OPMContext;

        public sealed override void InitHelpers()
        {
            base.InitHelpers();
            OPMContext = ((BaseController)(this.ViewContext.Controller)).OPMContext;
        }

        public sealed override void Write(object value)
        {
            Output.Write(value);
        }
    }

    /// <summary>
    /// PC前台视图页面基类型
    /// </summary>
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}