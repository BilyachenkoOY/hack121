using Hack121.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.InterfaceDefinitions.Providers
{
    public interface ITransactionDataProvider : IBaseDataProvider<PaymentTransaction>
    {
        IList<PaymentTransaction> GetPayerTransactions(string payerErdpo);

        void Create(IEnumerable<PaymentTransaction> entities);
    }
}
