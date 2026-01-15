using Cacx.LocalizationManager.Abstractions;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Cacx.LocalizationManager.Core;

/// <summary>
/// Provides access to localized resources for a specified culture using a resource manager. Supports dynamic updates to
/// the resource context and culture, and notifies listeners when localization data changes.
/// </summary>
/// <remarks>LocalizationProvider enables retrieval of localized strings, objects, and streams from resource
/// files, adapting to changes in culture or resource context at runtime. It implements INotifyPropertyChanged to allow
/// UI elements or other consumers to react to localization updates. This class is suitable for applications that
/// require dynamic localization, such as those supporting runtime language switching.
/// </remarks>
public class LocalizationProvider : ILocalizationProvider, INotifyPropertyChanged
{
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    /// <remarks>This event is typically raised by calling the OnPropertyChanged method after a property value
    /// is modified. It is commonly used to notify data binding clients that a property value has changed.
    /// </remarks>
    public event PropertyChangedEventHandler? PropertyChanged;
    private ResourceManager _resourceManager;
    private CultureInfo _culture;

    /// <summary>
    /// Gets the localized string associated with the specified resource key.
    /// </summary>
    /// <remarks>If the specified key does not exist in the resource manager, a placeholder string in the
    /// format "! MISSING KEY:{key}!" is returned. This indexer is culture-sensitive and retrieves resources based on
    /// the current culture.</remarks>
    /// <param name="key">The resource key for which to retrieve the localized string. Cannot be null.</param>
    /// <returns>The localized string corresponding to the specified key, or a placeholder indicating a missing key if the
    /// resource is not found.</returns>
    public string this[string key]
        => _resourceManager.GetString(key, _culture) ?? $"! MISSING KEY:{key}!";

    /// <summary>
    /// Initializes a new instance of the LocalizationProvider class using the specified resource name and culture.
    /// </summary>
    /// <remarks>Use this constructor to create a LocalizationProvider for a specific resource and culture. If
    /// the culture parameter is not provided, the provider defaults to the current thread's culture.</remarks>
    /// <param name="resourceName">The base name of the resource file containing localized strings. This value is used to locate the resource for
    /// localization.</param>
    /// <param name="culture">The culture to use for retrieving localized resources. If null, the current culture is used.</param>
    public LocalizationProvider(string resourceName, CultureInfo? culture)
    {
        _resourceManager = new ResourceManager(resourceName, Assembly.GetCallingAssembly());
        _culture = culture ?? CultureInfo.CurrentCulture;
    }

    /// <summary>
    /// Initializes a new instance of the LocalizationProvider class using the specified resource manager and culture.
    /// </summary>
    /// <remarks>Use this constructor to specify both the resource manager and the culture for localization.
    /// If the culture parameter is omitted or null, the provider defaults to using CultureInfo.CurrentCulture for
    /// resource retrieval.</remarks>
    /// <param name="resourceManager">The ResourceManager instance used to retrieve localized resources.</param>
    /// <param name="culture">The culture to use for resource lookups. If null, the current culture is used.</param>
    public LocalizationProvider(ResourceManager resourceManager, CultureInfo? culture)
    {
        _resourceManager = resourceManager;
        _culture = culture ?? CultureInfo.CurrentCulture;
    }

    /// <summary>
    /// Updates the current culture for the instance and notifies listeners of the change.
    /// </summary>
    /// <remarks>If the specified culture is the same as the current culture, or is <see langword="null"/>, no
    /// change occurs and no notification is sent. This method raises the <see cref="PropertyChanged"/> event for all
    /// properties to indicate that culture-dependent values may have changed.</remarks>
    /// <param name="culture">The new <see cref="CultureInfo"/> to apply. Cannot be <see langword="null"/> and must differ from the current
    /// culture to trigger an update.</param>
    public void UpdateCulture(CultureInfo culture)
    {
        if (culture is null || culture.Equals(_culture))
            return;

        _culture = culture;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }

    /// <summary>
    /// Updates the resource manager context to use resources identified by the specified base name.
    /// </summary>
    /// <remarks>Calling this method releases all previously loaded resources and reinitializes the resource
    /// manager. This will trigger a property change notification for all properties. Use this method when switching to
    /// a different set of resources at runtime.</remarks>
    /// <param name="resourceName">The base name of the resource file to associate with the resource manager. Cannot be null or empty.</param>
    public void UpdateContext(string resourceName)
    {
        _resourceManager.ReleaseAllResources();
        _resourceManager = new ResourceManager(baseName: resourceName, Assembly.GetCallingAssembly());
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }

    /// <summary>
    /// Replaces the current resource manager with the specified instance and releases all resources held by the
    /// previous manager.
    /// </summary>
    /// <remarks>This method releases all resources managed by the existing resource manager before updating
    /// to the new instance. After the context is updated, the <see cref="PropertyChanged"/> event is raised to notify
    /// listeners of the change.</remarks>
    /// <param name="resourceManager">The new <see cref="ResourceManager"/> instance to associate with the context. Cannot be null.</param>
    public void UpdateContext(ResourceManager resourceManager)
    {
        _resourceManager.ReleaseAllResources();
        _resourceManager = resourceManager;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }

    /// <summary>
    /// Retrieves the localized string associated with the specified resource key.
    /// </summary>
    /// <param name="key">The resource key used to identify the string to retrieve. Cannot be null.</param>
    /// <returns>The localized string corresponding to the specified key. If the key is not found, returns a placeholder string
    /// indicating the missing key.</returns>
    public string GetString(string key)
        => _resourceManager.GetString(key, _culture) ?? $"! MISSING KEY:{key}!";

    /// <summary>
    /// Retrieves an object resource associated with the specified key for the current culture.
    /// </summary>
    /// <remarks>This method searches for the resource using the current culture. If the resource is not
    /// found, an exception is thrown rather than returning null.</remarks>
    /// <param name="key">The key of the object resource to retrieve. Cannot be null or empty.</param>
    /// <returns>The object resource associated with the specified key.</returns>
    /// <exception cref="MissingManifestResourceException">Thrown if a resource with the specified key cannot be found.</exception>
    public object GetObject(string key)
        => _resourceManager.GetObject(key, _culture) ?? throw new MissingManifestResourceException($"Key '{key}' not found in resources.");

    /// <summary>
    /// Retrieves a resource stream associated with the specified key for the current culture.
    /// </summary>
    /// <param name="key">The key identifying the resource stream to retrieve. Cannot be null or empty.</param>
    /// <returns>A <see cref="Stream"/> containing the resource data associated with the specified key.</returns>
    /// <exception cref="MissingManifestResourceException">Thrown if a resource with the specified <paramref name="key"/> cannot be found.</exception>
    public Stream GetStream(string key)
        => _resourceManager.GetStream(key, _culture) ?? throw new MissingManifestResourceException($"Key '{key}' not found in resources.");

    /// <summary>
    /// Gets the culture information associated with the current instance.
    /// </summary>
    /// <returns>A <see cref="CultureInfo"/> object representing the culture settings in use.</returns>
    public CultureInfo GetCulture() => _culture;

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event to notify listeners that a property value has changed.
    /// </summary>
    /// <remarks>Override this method in a derived class to customize how property change notifications are
    /// raised.</remarks>
    /// <param name="propertyName">The name of the property that changed. Cannot be null or empty.</param>
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
