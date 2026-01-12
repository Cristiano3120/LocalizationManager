using Cacx.LocalizationManager.Abstractions;

namespace Cacx.LocalizationManager.Core;

public sealed class DesignTimeWindowContext
{
    public IDesignTimeLocalizationProvider Loc { get; } = new DesignTimeLocalizationProvider();
}
