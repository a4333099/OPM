using OPM.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Runtime.CompilerServices;
using OPM.Core.Helpers;

namespace OPM.Core.IocReg
{
    public class OPMContext
    {




        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine EngineInitialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = new OPMEngine();

                var config = OPMConfigManager.CreateConfig();
                Singleton<IEngine>.Instance.Initialize(config);
            }
            return Singleton<IEngine>.Instance;
        }


        public static IEngine CurrentEngine
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

        public static string Sid
        {
            get
            {
                return Utils.GetSidCookie() ?? Utils.SetSid();
            }
        }
    }
}
