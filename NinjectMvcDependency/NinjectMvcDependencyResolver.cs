using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace NinjectMvcDependency
{
    public class NinjectMvcDependencyResolver<T> : IDependencyResolver where T : class
    {
        private IKernel kernel;
        public NinjectMvcDependencyResolver()
        {
            this.kernel = new StandardKernel();
            this.kernel.Settings.InjectNonPublic = true;
            this.AddBindings();
        }
        private void AddBindings()
        {
            this.kernel.Bind<T>().To<T>();
        }
        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }
    }
}
