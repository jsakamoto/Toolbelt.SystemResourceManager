using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Toolbelt.SystemResourceManager.Internals;

internal class CascadingResourceManager : ResourceManager
{
	private readonly ResourceManager? _BaseManager;

	public CascadingResourceManager(ResourceManager? baseManager, string resourceName, Assembly resourceAssembly)
		: base(resourceName, resourceAssembly)
	{
		this._BaseManager = baseManager;
	}

	public override string? GetString(string name, CultureInfo culture)
	{
		return base.GetString(name, culture) ?? this._BaseManager?.GetString(name, culture);
	}
}
