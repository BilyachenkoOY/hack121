using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Managers
{
    public class TransactionManager : BaseEntityManager<Transaction, ITransactionDataProvider>
    {
        public TransactionManager(ITransactionDataProvider provider)
            : base(provider)
        {
        }

        protected override string Name
        {
            get { return "Transaction"; }
        }

        public IList<Transaction> GetPayerTransactions(string payerErdpo)
        {
            var key = PayerErdpoCacheKey(payerErdpo);
            return FromCache(key, () => Provider.GetPayerTransactions(payerErdpo));
        }

        public override void OnCreate(Transaction obj)
        {
            base.OnCreate(obj);
            RemoveFromCache(PayerErdpoCacheKey(obj.PayerEdrpo));
        }

        private string PayerErdpoCacheKey(string payerErdpo)
        {
            return GetListKey("erdpo", payerErdpo);
        }
    }
}
