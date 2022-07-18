using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.SystemResourceManager.Internals;

namespace Toolbelt.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for injecting a system resource manager.
/// </summary>
public static class SystemResourceManagerDependencyInjection
{
    /// <summary>
    /// Inject the resource manager as a system resource.
    /// </summary>
    /// <typeparam name="TResource">The resource class generated from a .resx file that you want to inject as a system resource.</typeparam>
    /// <param name="services">The instance of Microsoft.Extensions.DependencyInjection.IServiceCollection.</param>
    public static IServiceCollection AddSystemResourceManager<TResource>(this IServiceCollection services)
    {
        var t = typeof(TResource);
        return services.AddSystemResourceManager(t.FullName, t.Assembly);
    }

    /// <summary>
    /// Inject the resource manager as a system resource.
    /// </summary>
    /// <param name="services">The instance of Microsoft.Extensions.DependencyInjection.IServiceCollection.</param>
    /// <param name="resourceName">The name of the resource you want to inject as a system resource.</param>
    /// <param name="resourceAssembly">The assembly that contains the resource that you want to inject as a system resource.</param>
    public static IServiceCollection AddSystemResourceManager(this IServiceCollection services, string resourceName, Assembly resourceAssembly)
    {
        var typeofSR = typeof(ValidationAttribute).Assembly.GetType("System.SR");
        var s_resourceManager = typeofSR?.GetField("s_resourceManager", BindingFlags.NonPublic | BindingFlags.Static);
        if (s_resourceManager == null) return services;

        var baseResourceManager = s_resourceManager.GetValue(null) as ResourceManager;
        var resMan = new CascadingResourceManager(baseResourceManager, resourceName, resourceAssembly);
        s_resourceManager.SetValue(null, resMan);

        return services;
    }
}
