using Valour.Client.Shared.Windows.HomeWindows;

namespace Valour.Client.Windows;

public class HomeWindow : ClientWindow
{
    public override Type GetComponentType() =>
        typeof(HomeWindowComponent);
    public HomeWindow(int index) : base(index)
    {

    }
}