using OPM.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Runtime.CompilerServices;
using OPM.Core.Helpers;
using OPM.Core.Domain;

namespace OPM.Core.IocReg
{
    public class OPMContext
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public  IEngine EngineInitialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = new OPMEngine();

                var config = OPMConfigManager.CreateConfig();
                Singleton<IEngine>.Instance.Initialize(config);
            }
            return Singleton<IEngine>.Instance;
        }


        public  IEngine CurrentEngine
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    EngineInitialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }

        public  string Sid
        {
            get
            {
                return Utils.GetSidCookie() ?? Utils.SetSid();
            }
        }

        public  string Uid
        {
            get
            {
                return WebHelper.GetCookie("uid", -1);
            }
            set
            {
                WebHelper.SetCookie("uid",value);
            }
        }

       
    }
}
