using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Mvvm.ViewModels;

[QueryProperty(nameof(TextoReload), "Reload")]
public partial class TextoViewModel : ObservableObject
{
    public string TextoReload
    {
        set => _textoLoadAsync = TextoReloadAsync();
    }

    public Action<int>? IndexViewModel;

    private readonly ITextoService _textoService;

    private readonly IConfiguracoesService _configuracoesService;

    private CancellationTokenSource cts;

    public Task _textoLoadAsync { get; private set; }

    private int _index;

    [ObservableProperty]
    private Texto _linhasTexto;

    [ObservableProperty]
    private ImageSource _myImageSource;

    public TextoViewModel(ITextoService textoService, IConfiguracoesService configuracoesService)
    {
        _textoService = textoService;
        _configuracoesService = configuracoesService;
        _textoLoadAsync = TextoLoadAsync();

    }

    [RelayCommand]
    private async Task ButtonClickStartStopAsync(object imageSource)
    {
        if (MyImageSource.ToString() == "File: musicplayerstart.png")
        {
            MyImageSource = ImageSource.FromFile("musicplayerstop.png");
            _ = SpeakNowDefaultSettingsAsync();
        }
        else
        {
            MyImageSource = ImageSource.FromFile("musicplayerstart.png");
            CancelSpeech();
        }
    }

    [RelayCommand]
    private void ButtonClickPrevious()
    {
        if (_index > 0)
        {
            _index--;
            CancelSpeech();
            ChangedIndex(_index);
            _ = SpeakNowDefaultSettingsAsync();
        }
    }

    [RelayCommand]
    private void ButtonClickNext()
    {
        if (_index < LinhasTexto?.Linhas.Count)
        {
            _index++;
            CancelSpeech();
            ChangedIndex(_index);
            _ = SpeakNowDefaultSettingsAsync();
        }
    }

    private async Task SpeakNowDefaultSettingsAsync()
    {
        try
        {
            for (int i = _index; i < LinhasTexto.Linhas.Count; i++)
            {
                var config = await _configuracoesService.GetConfiguracoes();

                if (!string.IsNullOrEmpty(LinhasTexto.Linhas[i]))

                    await TextToSpeech.Default.SpeakAsync(LinhasTexto.Linhas[i],
                        new SpeechOptions()
                        {
                            Pitch = (float)config.Tom / 50,
                            Volume = (float)config.Volume / 100,
                            Locale = config.Local
                        },
                        cancelToken: cts.Token);

                if (!cts.Token.IsCancellationRequested)
                {
                    _index++;
                    ChangedIndex(_index);
                }
            }
        }
        catch (TaskCanceledException)
        {
            cts = new CancellationTokenSource();
        }
    }

    private void CancelSpeech()
    {
        if (cts?.IsCancellationRequested ?? true)
            return;

        cts.Cancel();
    }

    private void ChangedIndex(int index)
    {
        _textoService.UpdateIndex(index);
        IndexViewModel?.Invoke(index);
    }
    private async Task TextoReloadAsync()
    {
        LinhasTexto = await _textoService.GetTexto();
    }
    private async Task TextoLoadAsync()
    {
        MyImageSource = "musicplayerstart.png";
        cts = new CancellationTokenSource();
        LinhasTexto = await _textoService.GetTexto();
    }
}
