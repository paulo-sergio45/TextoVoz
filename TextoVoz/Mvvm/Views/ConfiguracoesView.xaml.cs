using TextoVoz.Mvvm.ViewModels;

namespace TextoVoz.Mvvm.Views;

public partial class ConfiguracoesView : ContentPage
{
    public ConfiguracoesView(ConfiguracoesViewModel configuracoesViewModel)
    {
        InitializeComponent();
        BindingContext = configuracoesViewModel;
    }
}