using AskGenerator.Business.InterfaceDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using System.Web;

using AskGenerator.Business.Entities;

namespace AskGenerator
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
