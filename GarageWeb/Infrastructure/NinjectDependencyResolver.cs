using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;

namespace GarageWeb.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelProgram)
        {
            kernel = kernelProgram;
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<IAuthHelper>().To<AdminAuthHelper>();
        }

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);

    }
}