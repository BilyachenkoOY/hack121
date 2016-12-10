using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Hack121.Mvc;
using Hack121.Business.InterfaceDefinitions;
using Hack121.Business.Managers;
using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using Hack121.NhibernateProvider.Providers;
using Hack121.NhibernateProvider.cs.Providers;


namespace Hack121.App_Start.Autofac
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(Application).Assembly);

            RegisterTypes(builder);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            //Entity providers section
            builder.RegisterType<NhibernateUniversityDataProvider>().As<IUniversityDataProvider>();
            builder.RegisterType<NhibernateTransactionDataProvider>().As<ITransactionDataProvider>();
            builder.RegisterType<NhibernatePaymentCategoryDataProvider>().As<IPaymentCategoryDataProvider>();
            //Entity managers section
            builder.RegisterInstance(new Site());
        }
    }
}
