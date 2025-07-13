using System;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Mvvm.ViewModels;

public partial class TextoViewModel(ITextoService textoService, IConfiguracoesService configuracoesService) : ObservableObject
{
    private readonly ITextoService _textoService = textoService;

    private readonly IConfiguracoesService _configuracoesService = configuracoesService;

    private CancellationTokenSource cts = new CancellationTokenSource();

    private int _index = 0;

    private ImageSource _startImageSource = ImageSource.FromFile("musicplayerstart.png");

    private ImageSource _stopImageSource = ImageSource.FromFile("musicplayerstop.png");

    [ObservableProperty]
    private Texto _linhasTexto;

    [ObservableProperty]
    private ImageSource _startStopImageSource;

    [ObservableProperty]
    private bool _isBusy = false;

    [ObservableProperty]
    private bool _startStop = false;

    [RelayCommand]
    private void ButtonClickStartStop(object imageSource)
    {
        StartStop = !StartStop;
        ChangeMyImageSource();
        ChangePlayer();

    }

    [RelayCommand]
    private void CollectionViewScrolled(ItemsViewScrolledEventArgs args)
    {
        int firstIndex = args.FirstVisibleItemIndex;
        int lastIndex = args.LastVisibleItemIndex;
        _index = firstIndex;

    }
    private async Task TextToSpeechAsync()
    {
        try
        {
            using (cts = new CancellationTokenSource())
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
                        ScrollToIndex(_index);
                        UpdateIndex(_index);
                    }
                }
            }
        }
        catch (TaskCanceledException)
        {

        }
    }

    private void CancelSpeech()
    {
        if (cts.IsCancellationRequested)
            return;

        cts.Cancel();
    }

    private void ScrollToIndex(int index)
    {
        WeakReferenceMessenger.Default.Send(new ScrollToIndexMessage(index));
    }

    private void UpdateIndex(int index)
    {
        _textoService.UpdateIndex(index);
    }

    private void ChangeMyImageSource()
    {
        if (StartStop)
            StartStopImageSource = _stopImageSource;
        else
            StartStopImageSource = _startImageSource;
    }

    private void ChangePlayer()
    {

        if (StartStop)
            _ = TextToSpeechAsync();
        else
            CancelSpeech();
    }

    public async Task InitializeAsync()
    {
        StartStopImageSource = _startImageSource;
        LinhasTexto = await _textoService.GetTexto();
        _index = _textoService.GetIndex();
        ScrollToIndex(_index);

    }

}