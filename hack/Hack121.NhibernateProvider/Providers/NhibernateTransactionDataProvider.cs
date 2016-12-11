using Hack121.NhibernateProvider.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using NHibernate.Criterion;

namespace Hack121.NhibernateProvider.cs.Providers
{
    public class NhibernateTransactionDataProvider : NhibernateBaseDataProvider<PaymentTransaction>, ITransactionDataProvider
    {
        public IList<PaymentTransaction> GetPayerTransactions(string payerErdpo)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<PaymentTransaction>();
                return criteria
                    .Add(Expression.Eq("PayerEdrpo", payerErdpo))
                    .List<PaymentTransaction>();
            });
        }
        public void Create(IEnumerable<PaymentTransaction> entities)
        {
            Execute(session =>
            {
                    foreach (var e in entities)
                    {
                        session.SaveOrUpdate(e);
                    }
                    session.Flush();
            });
        }
    }
}
