namespace ShopApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        LabelVersion.Text = $"Versión {AppInfo.VersionString} ({AppInfo.BuildString})";
    }
}
