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
    public class NhibernateTransactionDataProvider : NhibernateBaseDataProvider<Transaction>, ITransactionDataProvider
    {
        public IList<Transaction> GetPayerTransactions(string payerErdpo)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<Transaction>();
                return criteria
                    .Add(Expression.Eq("PayerEdrpo", payerErdpo))
                    .List<Transaction>();
            });
        }
    }
}
