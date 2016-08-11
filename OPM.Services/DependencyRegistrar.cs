using OPM.Core.IocReg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using OPM.Core.Config;
using OPM.Data;
using OPM.Core.Data;
using OPM.Core.Cache;
using OPM.Core.RandomsMethod;
using OPM.Services.Users;

namespace OPM.Services
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
            builder.RegisterGeneric(typeof(MongodbRep<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<RedisCache>().As<IOPMSessionCache>().InstancePerLifetimeScope();

            builder.RegisterType<UserInfoSer>().As<IUserInfoSer>().InstancePerLifetimeScope();

            builder.RegisterType<OPMRandom>().As<IOPMRandom>().InstancePerLifetimeScope();
        }
    }
}