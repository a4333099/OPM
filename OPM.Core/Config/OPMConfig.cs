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
    public class OPMConfig 
    {

        public string ConnetionString { get; set; }
        public string DbName { get; set; }


    }
}
