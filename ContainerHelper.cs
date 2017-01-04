using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeinboDesktop.Services;

namespace HeinboDesktop 
{
    public static class ContainerHelper
    {
        private static IUnityContainer _container;
         static ContainerHelper()
        {
            _container = new UnityContainer();
            _container.RegisterType<IProductService, ProductService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ICustomerService, CustomerService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IOrderService, OrderService>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }
    }
}
