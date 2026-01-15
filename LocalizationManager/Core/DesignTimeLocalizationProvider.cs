using Cacx.LocalizationManager.Abstractions;

namespace Cacx.LocalizationManager.Core;

/// <summary>
/// Provides a design-time implementation of the localization provider that returns placeholder values for resource
/// keys.
/// </summary>
/// <remarks></remarks>
public sealed class DesignTimeLocalizationProvider : IDesignTimeLocalizationProvider
{
    /// <summary>
    /// Returns a placeholder value. E.g. for key "HelloWorld" it will return "[HelloWorld]".
    /// </summary>
    /// <param name="key">The unique identifier. Cannot be <see langword="null"/> or empty</param>
    /// <returns></returns>
    public string this[string key]
        => $"[{key}]";
}