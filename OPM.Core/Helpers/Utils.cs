using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OPM.Core.Helpers
{
    public class Utils
    {
        /// <summary>
        /// 生成sessionid
        /// </summary>
        /// <returns></returns>
        public static string GenerateSid()
        {
            byte[] byteArray = Guid.NewGuid().ToByteArray();
            var i = byteArray.Aggregate<byte, long>(1, (current, b) => current * ((int)b + 1));
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public static string SetSid()
        {
            var sid = GetSidCookie();
            if (string.IsNullOrEmpty(sid))
            {
                sid = GenerateSid();
                SetCookie("sid", sid);
            }
            return sid;
        }

        public static string GetSidCookie()
        {
            return WebHelper.GetCookie("sid");
        }

        public static string GetCookiePassword()
        {
            var p = WebHelper.GetCookie("password");
            return WebHelper.UrlDecode(p);

        }

        public static void SetCookie(string key, object value)
        {
            if (HttpContext.Current == null) return;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key] ?? new HttpCookie(key);
            cookie.Value = value.ToString();
            cookie.Expires = DateTime.Now.AddMinutes(15);
            HttpContext.Current.Response.AppendCookie(cookie);
        }



        public static void SetPasswordCookie(string pwd, string enkey)
        {
            if (string.IsNullOrEmpty(pwd)) return;
            var p = WebHelper.UrlEncode(SecureHelper.AESEncrypt(pwd, enkey));
            SetCookie("password", p);
        }



    }
}
