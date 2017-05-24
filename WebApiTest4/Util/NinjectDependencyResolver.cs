using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using WebApiTest4.Models.ExamsModels;
using WebApiTest4.Services;
using WebApiTest4.Services.Impls;

namespace WebApiTest4.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            var context = new ExamAppDbContext();
            kernel.Bind<ITaskService>().To<TaskServiceImpl>().WithConstructorArgument("context", context);
            kernel.Bind<IUserService>().To<UserServiceImpl>().WithConstructorArgument("context", context);
            kernel.Bind<ITopicService>().To<TopicServiceImpl>().WithConstructorArgument("context", context);
            kernel.Bind<ISchoolService>().To<SchoolServiceImpl>().WithConstructorArgument("context", context);
        }
    }
}