using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Hach121.Mvc;
using Hach121.Business.InterfaceDefinitions;
using Hach121.Business.Managers;
using Hach121.Business.InterfaceDefinitions.Managers;
using Hach121.Business.Entities;

namespace Hach121.App_Start.Autofac
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
            
            //Entity managers section
            
        }
    }
}
