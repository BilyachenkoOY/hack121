using Hack121.Business.Entities;
using Hack121.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack121.Business.Managers
{
    public abstract class BaseEntityManager<T, TProvider> : BaseManager
        where T : Entity
        where TProvider : IBaseDataProvider<T>
    {
        protected TProvider Provider { get; set; }

        public BaseEntityManager(TProvider provider)
        {
            Provider = provider;
        }

        public T Get(string id)
        {
            var key = GetKey(id);
            return FromCache(key, () => Provider.Get(id));
        }

        public void Create(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            Provider.Create(obj);
            OnCreate(obj);
        }

        public virtual void OnCreate(T obj)
        {
            RemoveFromCache(GetListKey());
        }

        public IList<T> List()
        {
            var key = GetListKey();
            return FromCache(key, () => Provider.List());
        }

        public void Delete(string id)
        {
            Provider.Delete(id);
            OnDelete(id);
        }

        public virtual void OnDelete(string id)
        {
            RemoveFromCache(GetKey(id));
            RemoveFromCache(GetListKey());
        }
    }
}
