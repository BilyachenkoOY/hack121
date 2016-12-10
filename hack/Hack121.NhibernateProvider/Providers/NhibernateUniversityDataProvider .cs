using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.NhibernateProvider.Providers
{
    public class NhibernateUniversityDataProvider : NhibernateBaseDataProvider<University>, IUniversityDataProvider
    {
    }
}
