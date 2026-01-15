using Cacx.LocalizationManager.Core;

namespace SampleProject.MVVM;

internal sealed class MainWindowMVVM
{
    public LocalizationProvider Loc { get; }

    public MainWindowMVVM()
    {
        Loc = new LocalizationProvider(resourceName: "SampleProject.Resources.MainWindow.MainWindow", null);
    }
}
