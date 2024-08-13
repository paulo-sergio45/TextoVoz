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
            LoadTexto();
        }
    }

    private readonly ITextoRepository _textoRepository;

    private readonly IConfiguracoesRepository _configuracoesRepository;

    [ObservableProperty]
    Texto _linhasTexto;

    CancellationTokenSource cts;

    public Action<int>? IndexViewModel;

    private int _index;

    [ObservableProperty]
    ImageSource _myImageSource;
    public TextoViewModel(ITextoRepository textoRepository, IConfiguracoesRepository configuracoesRepository)
    {
        _textoRepository = textoRepository;
        _configuracoesRepository = configuracoesRepository;
        MyImageSource = "musicplayerstart.png";
        cts = new CancellationTokenSource();
        LoadTexto();
    }

    [RelayCommand]
    async Task ButtonClickStartStopAsync(object imageSource)//teste CommandParameter passando imageSource viewmodel
    {

        if (MyImageSource.ToString() == "File: musicplayerstart.png")
        {

            MyImageSource = ImageSource.FromFile("musicplayerstop.png");
            SpeakNowDefaultSettingsAsync();
        }
        else
        {
            MyImageSource = ImageSource.FromFile("musicplayerstart.png");
            CancelSpeech();

        }

    }

    [RelayCommand]
    void ButtonClickPrevious()
    {
        if (_index > 0)
        {
            _index--;
            CancelSpeech();
            ChangedIndex(_index);
            SpeakNowDefaultSettingsAsync();

        }



    }

    [RelayCommand]
    void ButtonClickNext()
    {
        if (_index < LinhasTexto.Linhas.Count)
        {
            _index++;
            CancelSpeech();
            ChangedIndex(_index);
            SpeakNowDefaultSettingsAsync();

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
                    await TextToSpeech.Default.SpeakAsync(LinhasTexto.Linhas[i], new SpeechOptions()
                    {
                        Pitch = (float)config.Tom / 50,   // 0.0 - 2.0
                        Volume = (float)config.Volume / 100, // 0.0 - 1.0
                        Locale = config.Local

                    }, cancelToken: cts.Token);

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

    private void LoadTexto()
    {
        LinhasTexto = _textoRepository.GetTexto();
    }

}
