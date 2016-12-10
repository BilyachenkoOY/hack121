using Hach121.App_Start;
using Hach121.App_Start.Autofac;
using Hach121.App_Start.AutoMapper;
using Hach121.App_Start.Routes;
using Hach121.Core.Binders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hach121.Mvc
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.DefaultNamespaces.Add("Hach121.Mvc.Controllers");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.RegisterDependencies();
            AutoMapperConfig.Configure();
            SetModelBinders();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

        }

        private void SetModelBinders()
        {
            var floarBinder = new FloatBinder();
            ModelBinders.Binders.Add(typeof(decimal), floarBinder);
            ModelBinders.Binders.Add(typeof(decimal?), floarBinder);

            ModelBinders.Binders.Add(typeof(float), floarBinder);
            ModelBinders.Binders.Add(typeof(float?), floarBinder);

            ModelBinders.Binders.Add(typeof(double), floarBinder);
            ModelBinders.Binders.Add(typeof(double?), floarBinder);
        }
    }
}
