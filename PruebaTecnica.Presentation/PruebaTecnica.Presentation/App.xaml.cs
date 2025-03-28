using Microsoft.Identity.Client;
using PruebaTecnica.Presentation.Navigation;

namespace PruebaTecnica.Presentation
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            SwitchTheme(AppTheme.Light);

            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }

        public static void SwitchTheme(AppTheme theme)
        {
            App.Current!.UserAppTheme = theme;
        }
    }
}