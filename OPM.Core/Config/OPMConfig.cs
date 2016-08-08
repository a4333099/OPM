using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using OPM.Core.Helpers;

namespace OPM.Core.Config
{
    [Serializable]
    public class OPMConfig : IOPMConfig
    {


        private static string _configpath;

        //   public XElement Config { get { return _config; } set { _config = value; } }
        private  string GetPath(string filename)
        {
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Server.MapPath(filename);
            }
            return string.Empty;

        }

        public  OPMConfig CreateConfig()
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



        public string ConnetionString { get; set; }
        public string DbName { get; set; }

    }
}
