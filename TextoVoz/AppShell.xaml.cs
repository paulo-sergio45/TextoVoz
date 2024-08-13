using TextoVoz.Mvvm.Views;

namespace TextoVoz
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(TextoView), typeof(TextoView));
            Routing.RegisterRoute(nameof(ConfiguracoesView), typeof(ConfiguracoesView));
        }
    }
}
