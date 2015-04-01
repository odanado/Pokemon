using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public delegate void Func();
        private Func func;
        public ObservableDictionary(Func func)
        {
            this.func = func;
        }

        new public TValue this[TKey key]
        {
            set
            {
                if (this.ContainsKey(key))
                {
                    this.Remove(key);
                }
                this.Add(key, value);

                func();
            }
            get
            {
                TValue val;
                this.TryGetValue(key, out val);
                return val;
            }
        }
    }
}
