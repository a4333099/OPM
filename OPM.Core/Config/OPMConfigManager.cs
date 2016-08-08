using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OPM.Core.Config
{
    public class OPMConfigManager
    {
        static OPMConfigManager()
        {
            _configpath = GetPath(ConfigurationManager.AppSettings["ConfigPath"].ToString());
        }
        private static string _configpath;

        private static XElement _config;
     //   public XElement Config { get { return _config; } set { _config = value; } }
        private static string GetPath(string filename)
        {
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Server.MapPath(filename);
            }
            return string.Empty;

        }

        public static OPMConfig CreateConfig()
        {
            OPMConfig opmconfig = new OPMConfig();

            if (!string.IsNullOrEmpty(_configpath))
            {
                 _config = XElement.Load(_configpath);
                opmconfig.XeConfig = _config;
            }

            return opmconfig;
        }
    }
}
