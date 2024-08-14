using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Mvvm.ViewModels
{
    public partial class ConfiguracoesViewModel : ObservableObject
    {
        private readonly IConfiguracoesService _configuracoesService;

        [ObservableProperty]
        private double _tomVM;

        [ObservableProperty]
        private double _volumeVM;

        [ObservableProperty]
        private Locale _localVM;

        [ObservableProperty]
        private List<Locale> _locaisVM;
        public ConfiguracoesViewModel(IConfiguracoesService configuracoesService)
        {
            _configuracoesService = configuracoesService;
            _ = ConfiguracoesLoadAsync();
        }

        [RelayCommand]
        private async Task SaveConfiguracoesAsync()
        {
            if (LocalVM != null)
                _configuracoesService.AtualizaConfiguracoes(new Configuracoes { Volume = VolumeVM, Tom = TomVM, Local = LocalVM });

            await ConfiguracoesLoadAsync();
        }

        private async Task ConfiguracoesLoadAsync()
        {
            Configuracoes config = await _configuracoesService.GetConfiguracoes();
            VolumeVM = config.Volume;
            TomVM = config.Tom;
            LocalVM = config.Local;
            LocaisVM = await _configuracoesService.GetVozlocaisAsync();
        }
    }
}
