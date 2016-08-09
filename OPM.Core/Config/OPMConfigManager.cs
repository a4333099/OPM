using OPM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM.Core.Config
{
    public class OPMConfigManager
    {
        private static string _configpath;
       
        private static  string GetPath(string filename)
        {
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Server.MapPath(filename);
            }
            return string.Empty;

        }

        public static  OPMConfig CreateConfig()
        {
            OPMConfig opmconfig = new OPMConfig();
            _configpath = GetPath(ConfigurationManager.AppSettings["ConfigPath"].ToString());

            if (!string.IsNullOrEmpty(_configpath))
            {
                opmconfig = (OPMConfig)IOHelper.DeserializeFromXML(typeof(OPMConfig), _configpath);
                return opmconfig;
            }
            else
            {
                throw new OPMException("OPM配置路径异常");
            }

        }
    }
}
