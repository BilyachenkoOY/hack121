using Hack121.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.InterfaceDefinitions.Providers
{
    public interface IBaseDataProvider<T>
        where T: Entity
    {
        T Get(string id);

        void Create(T obj);

        IList<T> List();

        void Delete(string id);
    }
}
