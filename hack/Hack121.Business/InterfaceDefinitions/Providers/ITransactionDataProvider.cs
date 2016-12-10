using Hack121.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.InterfaceDefinitions.Providers
{
    public interface ITransactionDataProvider : IBaseDataProvider<Transaction>
    {
        IList<Transaction> GetPayerTransactions(string payerErdpo);
    }
}
