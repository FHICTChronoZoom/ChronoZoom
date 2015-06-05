using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business
{
    public class ApplicationSettings : IApplicationSettings
    {
        private readonly IDictionary<string, object> dictionary;

        public ApplicationSettings()
        {
            this.dictionary = new Dictionary<string, object>();
        }

        public void Set(string key, object value)
        {
            dictionary.Add(key, value);
        }

        public object Get(string key)
        {
            return dictionary[key];
        }
    }
}
