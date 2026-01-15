using Cacx.LocalizationManager.Abstractions;

namespace Cacx.LocalizationManager.Core;

/// <summary>
/// Provides design-time context for window components. Implementing this class allows for text during design time.
/// <para>
/// Every localization binding will show the key as text at design time.
/// </para>
/// </summary>
public sealed class DesignTimeWindowContext
{
    /// <summary>
    /// This property is gonna be used by the xaml designer to provide design-time localization.
    /// It will always return the key as the localized text.
    /// </summary>
    public IDesignTimeLocalizationProvider Loc { get; } = new DesignTimeLocalizationProvider();
}
