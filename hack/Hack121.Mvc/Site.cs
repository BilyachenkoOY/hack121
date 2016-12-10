using Hack121.Business.InterfaceDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hack121.Business.InterfaceDefinitions.Managers;
using System.Web;

using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;

namespace Hack121
{
    public static class Site
    {

        public static class Provider
        {
            public static IUniversityDataProvider University { get { return Get<IUniversityDataProvider>(); } }

            public static ITransactionDataProvider Transaction { get { return Get<ITransactionDataProvider>(); } }

            public static IPaymentCategoryDataProvider PaymentCategory { get { return Get<IPaymentCategoryDataProvider>(); } }
        }

        #region private
        private static T Get<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
        #endregion
    }
}
