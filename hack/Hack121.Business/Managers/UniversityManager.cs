using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Managers
{
    public class UniversityManager : BaseEntityManager<University, IUniversityDataProvider>
    {
        public UniversityManager(IUniversityDataProvider provider)
            : base(provider)
        {
        }
        protected override string Name
        {
            get { return "University"; }
        }
    }
}
