using Hack121.Business.Entities;
using Hack121.Business.Import.Parsers;
using Hack121.Business.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hack121.Business.Import
{
    public class TransactionsImport
    {
        TransactionManager manager;
        public TransactionsImport()
        {
            manager = DependencyResolver.Current.GetService<TransactionManager>();
        }
        protected string GetApiUrl (string erdpoCode, DateTime? from = null, DateTime? to = null)
        {
            if (!from.HasValue)
                from = DateTime.Today.AddDays(-DateTime.Today.DayOfYear + 1);
            if (!to.HasValue)
                to = DateTime.Today;
            return "http://www.007.org.ua/api/export-transactions-with-params?from={0:yyyy-MM-dd}&to={1:yyyy-MM-dd}&offset=NaN&who={2}"
                .FormatWith(from, to, erdpoCode);
        }

        public virtual void Import(string erdpoCode)
        {
            var stream = new WebClient().OpenRead(GetApiUrl(erdpoCode));
            // FileStream stream = File.OpenRead("D:\\transactions_2016_12_10.csv"); // look for API
            if (stream == null)
                return;
            IList<PaymentTransaction> transactions;
            using(var streamReader = new StreamReader(stream, Encoding.GetEncoding(1251)))
                transactions = new TransactionsParser007().Parse(streamReader);
            manager.Create(transactions);
        }
    }
}
