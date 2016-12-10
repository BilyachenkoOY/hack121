using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Entities
{
    public class Transaction : Entity
    {
        public virtual string TransactionId { get; set; }

        public virtual string PayerEdrpo { get; set; }

        public virtual decimal Price { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string CategoryId { get; set; }

        public virtual string ReceiverTitle { get; set; }
    }
}
