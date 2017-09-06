using System;
using System.Reflection;

namespace Startup
{
   
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
    public sealed class ProviderStartupAttribute : Attribute
    {
        
        public ProviderStartupAttribute(Type providerStartupType)
        {
            if (providerStartupType == null)
            {
                throw new ArgumentNullException(nameof(providerStartupType));
            }

            if (!typeof(IProvidersStartup).GetTypeInfo().IsAssignableFrom(providerStartupType.GetTypeInfo()))
            {
                throw new ArgumentException($@"""{providerStartupType}"" does not implement {typeof(IProvidersStartup)}.", nameof(providerStartupType));
            }

            ProviderStartupType = providerStartupType;
        }

        public Type ProviderStartupType { get; }
    }
}
