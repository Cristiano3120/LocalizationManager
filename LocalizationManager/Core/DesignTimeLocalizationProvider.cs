using Cacx.LocalizationManager.Abstractions;

namespace Cacx.LocalizationManager.Core;

public sealed class DesignTimeLocalizationProvider : IDesignTimeLocalizationProvider
{
    public string this[string key]
        => $"[{key}]";
}
