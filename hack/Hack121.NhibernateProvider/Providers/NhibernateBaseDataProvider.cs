using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.NhibernateProvider.Providers
{
    public class NhibernateBaseDataProvider<T> : IBaseDataProvider<T>
         where T : Entity
    {

        public T Get(string id)
        {
            return Execute<T>(session =>
            {
                return session.Get<T>(id);
            });
        }

        public void Create(T obj)
        {
            Execute(session =>
            {
                using(var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(obj);
                    transaction.Commit();
                }               
            });
        }

        public IList<T> List()
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<T>();
                
                return criteria.List<T>();
            });
        }

        public void Delete(string id)
        {
            Execute(session =>
            {
                using(var transaction = session.BeginTransaction())
                {
                    var valueToBeRemoved = session.Get<T>(id);
                    if (valueToBeRemoved != null)
                    {
                        session.Delete(valueToBeRemoved);
                        transaction.Commit();
                    }
                }
            });
        }

        #region helpers
        protected T Execute<T>(Func<ISession, T> expression)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                return expression(session);
            }
        }

        protected bool Execute(Func<ISession, bool> expression)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                return expression(session);
            }
        }

        protected void Execute(Action<ISession> expression)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                expression(session);
            }
        }


        #endregion
    }
}
