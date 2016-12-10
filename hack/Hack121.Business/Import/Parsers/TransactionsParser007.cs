using CSVFile;
using Hack121.Business.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Import.Parsers
{
    public class TransactionsParser007
    {
        public IList<Transaction> Parse(StreamReader stream)
        {
            var results = new List<Transaction>();
            var reader = new CSVReader(stream, delim: ';', first_row_are_headers: true);
            foreach(var line in reader.Lines()) {
                var transaction = ParseTransaction(line);
                if(transaction != null)
                    results.Add(transaction);
            }

            return results;
        }

        protected Transaction ParseTransaction(IList<string> row)
        {
            // trans_id;trans_date;payer_edrpou;payer_name;payer_mfo;payer_bank;recipt_edrpou;recipt_name;recipt_mfo;recipt_bank;amount;payment_details
            var result = new Transaction();
            result.PayerEdrpo = row[0];
            result.Date = DateTime.Parse(row[1], CultureInfo.GetCultureInfo(1251));
            result.PayerEdrpo = row[2];
            result.ReceiverTitle = row[7];
            result.Price = decimal.Parse(row[10]);
            result.PaymentDetails = row[11];

            //if (IsAnyEmpty(result.PayerEdrpo, result.PayerEdrpo, result.ReceiverTitle, result.PaymentDetails))
            //    return null;

            return result;
        }

        private bool IsAnyEmpty(params string[] values)
        {
            return values.Any(v => v.IsEmpty());
        }
    }
}
