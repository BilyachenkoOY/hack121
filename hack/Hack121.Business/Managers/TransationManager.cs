using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Managers
{
    public class TransactionManager : BaseEntityManager<PaymentTransaction, ITransactionDataProvider>
    {
        public TransactionManager(ITransactionDataProvider provider)
            : base(provider)
        {
        }

        protected override string Name
        {
            get { return "Transaction"; }
        }

        public IList<PaymentTransaction> GetPayerTransactions(string payerErdpo)
        {
            var key = PayerErdpoCacheKey(payerErdpo);
            return FromCache(key, () => Provider.GetPayerTransactions(payerErdpo));
        }


        public void Create(IList<PaymentTransaction> entities)
        {
            var count = entities.Count;
            var query = entities.AsEnumerable();
            while (count >= 10)
            {
                Provider.Create(query.Take(10));
                query = query.Skip(10);
                count -= 10;
            }
            if (count > 0)
                Provider.Create(query);
            OnCreate(entities.First());
        }

        public override void OnCreate(PaymentTransaction obj)
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
