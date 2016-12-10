using Hack121.Business.Entities;
using Hack121.Business.Import.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Import
{
    public class TransactionsImport
    {
        public virtual void Import(string erdpoCode)
        {
            var stream = File.OpenRead("D:\\transactions_2016_12_10.csv"); // look for API
            IList<Transaction> transactions;
            using(var streamReader = new StreamReader(stream))
                transactions = new TransactionsParser007().Parse(streamReader);
            if(transactions.Count < 5)
                throw new ArgumentException("erpo");
        }
    }
}
