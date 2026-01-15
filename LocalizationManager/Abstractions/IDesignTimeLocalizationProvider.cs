using Cacx.LocalizationManager.Core;

namespace Cacx.LocalizationManager.Abstractions;

/// <summary>
/// A dummy localization provider for design-time support.
/// </summary>
public interface IDesignTimeLocalizationProvider
{
    /// <summary>
    /// Dummy indexer to provide design-time localization support.
    /// Any class that implements this interface should return the key itself.
    /// Example implementer: <see cref="DesignTimeLocalizationProvider"/>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    string this[string key] { get; }
}
