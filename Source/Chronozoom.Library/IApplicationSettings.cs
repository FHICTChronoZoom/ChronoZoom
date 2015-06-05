using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business
{
    public interface IApplicationSettings
    {
        object Get(string key);
    }

    public static class ApplicationSettingsExtensions
    {
        public static T Get<T>(this IApplicationSettings settings, string key)
        {
            try
            {
                var value = settings.Get(key);
                if (value is Lazy<T>)
                {
                    return ((Lazy<T>)value).Value;
                }
                else
                {
                    return (T)value;
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
