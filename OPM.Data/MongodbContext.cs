using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OPM.Core.Data;
using OPM.Core.Config;

namespace OPM.Data
{
    public class MongodbContext : IOPMDbContext
    {
        public MongodbContext(OPMConfig opmconfig)
        {
            _opmconfig = opmconfig;

        }
        OPMConfig _opmconfig;
        public string ConnectionString
        {
            get
            {
                return "";
                 
            }

         
        }

        public string DbName
        {
            get
            {
                throw new NotImplementedException();
            }

           
        }

        public string Pwd
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Uid
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
