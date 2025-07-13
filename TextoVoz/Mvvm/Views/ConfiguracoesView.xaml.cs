using TextoVoz.Mvvm.ViewModels;

namespace TextoVoz.Mvvm.Views;

public partial class ConfiguracoesView : ContentPage
{
    protected private ConfiguracoesViewModel _configuracoesViewModel;
    public ConfiguracoesView(ConfiguracoesViewModel configuracoesViewModel)
    {
        InitializeComponent();
        BindingContext = configuracoesViewModel;
        _configuracoesViewModel = configuracoesViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _configuracoesViewModel.ConfiguracoesLoadAsync();
    }

}