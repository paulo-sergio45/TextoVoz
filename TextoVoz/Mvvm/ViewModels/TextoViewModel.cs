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
        set
        {
            LinhasTexto = LoadTexto();
        }
    }

    public Action<int>? IndexViewModel;

    private readonly ITextoRepository _textoRepository;

    private readonly IConfiguracoesRepository _configuracoesRepository;

    private CancellationTokenSource cts;

    private int _index;

    [ObservableProperty]
    private Texto _linhasTexto;

    [ObservableProperty]
    private ImageSource _myImageSource;

    public TextoViewModel(ITextoRepository textoRepository, IConfiguracoesRepository configuracoesRepository)
    {
        _textoRepository = textoRepository;
        _configuracoesRepository = configuracoesRepository;
        MyImageSource = "musicplayerstart.png";
        cts = new CancellationTokenSource();
        LinhasTexto = LoadTexto();
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
                var config = await _configuracoesRepository.GetConfiguracoesAsync();

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
        IndexViewModel?.Invoke(index);
    }

    private Texto LoadTexto()
    {
        return _textoRepository.GetTexto();
    }
}
