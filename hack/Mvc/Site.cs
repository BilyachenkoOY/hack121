using Hach121.Business.InterfaceDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hach121.Business.InterfaceDefinitions.Managers;
using System.Web;

using Hach121.Business.Entities;

namespace Hach121
{
    public static class Site
    {

        #region private
        private static T Get<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
        #endregion
    }
}
