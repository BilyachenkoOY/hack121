using Hack121.Business.InterfaceDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using Hack121.Business.Managers;

namespace Hack121
{
    public class Site
    {
        public Site()
        {

        }
        public static Site Current { get { return Get<Site>(); } }

        public CategoryManager Category { get { return Get<CategoryManager>(); } }

        public TransactionManager Transaction { get { return Get<TransactionManager>(); } }

        public UniversityManager University { get { return Get<UniversityManager>(); } }


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
