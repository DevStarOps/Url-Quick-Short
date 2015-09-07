using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Url_Quick_Short.Integrate
{
    public static class CommonIntegrationHelper
    {
        public static string ApplicationDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).TrimEnd('\\');

        private static List<IIntegration> providers = null;

        public static List<IIntegration> GetProviders()
        {
            if (providers != null)
            {
                return providers;
            }
            providers = new List<IIntegration>();
            foreach (var assemblyFile in Directory.GetFiles(ApplicationDirectory, "Url Quick Short.Integrate.*.dll"))
            {
                foreach (var type in Assembly.LoadFrom(assemblyFile).GetTypes())
                {
                    if (type.GetInterface("IIntegration") != null)
                    {
                        IIntegration item = Activator.CreateInstance(type) as IIntegration;
                        providers.Add(item);
                    }
                }
            }
            return providers;
        }
    }
}
