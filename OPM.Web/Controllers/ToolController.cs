using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OPM.Core.Cache;
using OPM.Core.Helpers;
using OPM.Core.IocReg;
using OPM.Core.RandomsMethod;
using OPM.Web.Framework;
using OPM.Web.Framework.Controllers;

namespace OPM.Web.Controllers
{
    public class ToolController : BaseController
    {
        public ToolController()
        {
            _cache = OPMContext.CurrentEngine.Resolve<IOPMSessionCache>();
            _random = OPMContext.CurrentEngine.Resolve<IOPMRandom>();
        }
        private IOPMSessionCache _cache;
        private IOPMRandom _random;
        public ImageResult VerifyImage(int width = 56, int height = 20)
        {
            Utils.SetSid();
            var sid = Utils.GetSidCookie();
            //生成验证值
            string verifyValue = _random.CreateRandomValue(4, false).ToLower();
            //生成验证图片
            RandomImage verifyImage = _random.CreateRandomImage(verifyValue, width, height, Color.White, Color.Blue, Color.DarkRed);
            //将验证值保存到session中
            _cache.Set(sid, "verifyCode", verifyValue);

            //输出验证图片
            return new ImageResult(verifyImage.Image, verifyImage.ContentType);
        }
    }
}