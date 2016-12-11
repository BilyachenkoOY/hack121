using Hack121.App_Start;
using Hack121.App_Start.Autofac;
using Hack121.App_Start.AutoMapper;
using Hack121.App_Start.Routes;
using Hack121.Core.Binders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hack121.Mvc
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.DefaultNamespaces.Add("Hack121.Mvc.Controllers");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.RegisterDependencies();
            AutoMapperConfig.Configure();
            SetModelBinders();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("uk-ua");
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("uk-ua");
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
