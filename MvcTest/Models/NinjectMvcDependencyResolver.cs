using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace MvcTest.Models
{
    public class NinjectMvcDependencyResolver : IDependencyResolver
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
            this.kernel.Bind<ILogic>().To<Logic>();
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
