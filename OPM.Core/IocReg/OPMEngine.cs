using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPM.Core.Config;
using Autofac;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace OPM.Core.IocReg
{
    public class OPMEngine : IEngine
    {

        private ContainerManager _containerManager;
        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        public void Initialize(OPMConfig config)
        {
            RegisterDependencies(config);
        }
        protected virtual void RegisterDependencies(OPMConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            this._containerManager = new ContainerManager(container);
            var typeFinder = new WebAppTypeFinder();
            builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<OPMConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

    }
}
