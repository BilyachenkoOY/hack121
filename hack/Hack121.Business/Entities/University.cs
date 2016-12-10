using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Entities
{
    public class University : Entity
    {
        public virtual string Edrpou { get; set; }

        public virtual string Title { get; set; }

        public virtual decimal YearBudget { get; set; }
    }
}
