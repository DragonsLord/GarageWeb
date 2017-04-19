using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using Ninject.Web.Common;
using GarageWeb.Models.Interfaces;
using GarageWeb.Models;
using GarageWeb.Models.Repositories;

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
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            kernel.Bind<UserManager<ApplicationUser>>().ToSelf();
            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            kernel.Bind<IRepository<Dish>>().To<DishesRepository>();
            kernel.Bind<IRepository<NewsEntry>>().To<NewsLine>();
        }

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);

    }
}