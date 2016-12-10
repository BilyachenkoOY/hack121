using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.NhibernateProvider
{
    public static class NhibernateHelper
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration().Configure();
            var sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
