using GoodPass.Presentation;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Uno.Resizetizer;

namespace GoodPass;
public sealed partial class AppHead : App
{
    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public AppHead()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        MainWindow.SetWindowIcon();
#if __ANDROID__
#endif
#if __HAS_UNO_SKIA_GTK__
#endif
#if WINDOWS
        MainWindow.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        MainWindow.AppWindow.TitleBar.BackgroundColor = Colors.Transparent;
        MainWindow.AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
        MainWindow.SetTitleBar(((Shell)(MainWindow.Content)).AppTitleBar);
#endif
    }
}
