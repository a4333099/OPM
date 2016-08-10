using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPM.Core.Config;
using Autofac;
using System.Web.Mvc;


namespace OPM.Core.IocReg
{
    public class OPMEngine : IEngine
    {

        IContainer _container;

        public void Initialize(OPMConfig config)
        {
            RegisterDependencies(config);
        }
        protected virtual void RegisterDependencies(OPMConfig config)
        {
            var builder = new ContainerBuilder();
             _container = builder.Build();
           
            var typeFinder = new WebAppTypeFinder();
            builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<OPMConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(_container);

            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder, config);
            builder.Update(_container);

            // DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

    }
}
