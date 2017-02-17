using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Managers
{
    public class CategoryManager : BaseEntityManager<PaymentCategory, IPaymentCategoryDataProvider>
    {

        public CategoryManager(IPaymentCategoryDataProvider provider)
            : base(provider)
        {

        }

        public Dictionary<string, string> Dictionary()
        {
            var key = GetKey("dict");
            return FromCache(key, () => Provider.List().ToDictionary(c => c.Id, c => c.Title));
        }

        public override void OnCreate(PaymentCategory obj)
        {
            base.OnCreate(obj);
            RemoveFromCache(GetKey("dict"));
        }

        public override void OnDelete(string id)
        {
            base.OnDelete(id);
            RemoveFromCache(GetKey("dict"));
        }

        protected override string Name
        {
            get { return "Category"; }
        }
    }
}
