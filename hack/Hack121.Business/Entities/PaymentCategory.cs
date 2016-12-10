using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Entities
{
    public class PaymentCategory : Entity
    {
        public virtual string Title { get; set; }

        public virtual string Keywords { get; set; }
    }
}
