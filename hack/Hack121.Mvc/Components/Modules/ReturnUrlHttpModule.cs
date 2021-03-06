﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Hack121.Mvc.Modules
{
    public class ReturnUrlHttpModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication application)
        {
            application.PreRequestHandlerExecute +=
                (new EventHandler(this.Application_BeginRequest));
        }

        private void Application_BeginRequest(Object source,
             EventArgs e)
        {
            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string returnUrl = null;
            foreach (var key in context.Request.Params.AllKeys)
            {
                if (key.Equals("returnUrl", StringComparison.InvariantCultureIgnoreCase))
                {
                    returnUrl = key;
                    break;
                }
            }
            if (!returnUrl.IsEmpty())
            {
                var url = context.Request.Params[returnUrl];
                ((MvcHandler)context.Handler).RequestContext.RouteData.Values["returnUrl"] = url;
            }
        }
    }
}
