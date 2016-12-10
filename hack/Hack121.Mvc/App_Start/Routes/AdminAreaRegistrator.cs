using Hack121.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hack121.App_Start.Routes
{
    public class AdminAreaRegistrator : BaseAreaRegistrator
    {
        public override string AreaName
        {
            get { return "Admin"; }
        }
        protected override string[] namespaces { get { return new[] { "Hack121.Controllers.Admin" }; } }
        protected override object defaultsComponets { get { return new { controller = "Home", action = "Index", id = UrlParameter.Optional }; } }

        public override void RegisterArea(AreaRegistrationContext registrationContext)
        {
            base.RegisterArea(registrationContext);

            MapRoute(
                name: "_Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
