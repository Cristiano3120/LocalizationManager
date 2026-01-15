using Cacx.LocalizationManager.Core;
using System.Globalization;
using System.Resources;

namespace Cacx.LocalizationManager.Abstractions;

/// <summary>
/// Defines a contract for retrieving localized resources, such as strings, objects, and streams, and for managing
/// localization context and culture settings.
/// </summary>
/// <remarks>Implementations of this interface provide mechanisms to access localized resources based on a
/// specified key and to update the underlying resource context or culture. This interface is typically used to support
/// internationalization and localization in applications by abstracting resource retrieval and culture
/// management.</remarks>
public interface ILocalizationProvider
{
    /// <summary>
    /// Gets the localized string for the specified key.
    /// </summary>
    /// <param name="key">The unique identifier to look for in the resource file. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see langword="string"/> corresponding to the entered key and current culture info</returns>
    string GetString(string key);

    /// <summary>
    /// Retrieves the <see langword="object"/> associated with the specified key.
    /// </summary>
    /// <param name="key">The unique identifier to look for in the resource file. Cannot be <see langword="null"/>.</param>
    /// <returns>
    /// The <see langword="object"/> associated with the specified key. <br></br>
    /// Default implementation(<see cref="LocalizationProvider"/>) throws an <see cref="MissingManifestResourceException"/> if <see langword="null"/>
    /// </returns>
    object GetObject(string key);

    /// <summary>
    /// Retrieves the <see cref="Stream"/> associated with the specified key.
    /// </summary>
    /// <param name="key">The unique identifier to look for in the resource file. Cannot be <see langword="null"/>.</param>
    /// <returns>
    /// The <see cref="Stream"/> associated with the specified key. <br></br>
    /// Default implementation(<see cref="LocalizationProvider"/>) throws an <see cref="MissingManifestResourceException"/> if <see langword="null"/>
    /// </returns>
    Stream GetStream(string key);

    /// <summary>
    /// Updates the current context to use the specified <see cref="ResourceManager"/> for subsequent operations.
    /// You have to call this method when your data context changes, 
    /// such as when you switch from the login screen to the main application.
    /// </summary>
    /// <param name="resourceManager">The <see cref="ResourceManager"/> to set as the current context. Cannot be <see langword="null"/>.</param>
    void UpdateContext(ResourceManager resourceManager);

    /// <summary>
    /// Updates the current operational context to use the specified resource name.
    /// You have to call this method when your data context changes, 
    /// such as when you switch from the login screen to the main application.
    /// </summary>
    /// <param name="resourceName">The name of the resource to set as the current context. Cannot be <see langword="null"/> or empty.</param>
    void UpdateContext(string resourceName);

    /// <summary>
    /// Updates the current culture settings to the specified culture.
    /// This acts as an language switch. The UI will be updated to reflect the new culture.
    /// </summary>
    /// <param name="culture">The culture to apply to the current context. Cannot be <see langword="null"/>.</param>
    void UpdateCulture(CultureInfo culture);

    /// <summary>
    /// Gets the culture information.
    /// </summary>
    /// <returns>A <see cref="CultureInfo"/> instance representing the current culture settings.</returns>
    CultureInfo GetCulture();
}
