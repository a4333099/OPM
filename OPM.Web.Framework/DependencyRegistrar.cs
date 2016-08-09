using OPM.Core.IocReg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using OPM.Core.Config;

namespace OPM.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, OPMConfig config)
        {
            
        }
    }
}