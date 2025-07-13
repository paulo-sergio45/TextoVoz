using System;
using CommunityToolkit.Mvvm.Messaging;
using TextoVoz.Mvvm.Models;
using TextoVoz.Mvvm.ViewModels;

namespace TextoVoz.Mvvm.Views;

public partial class TextoView : ContentPage, IRecipient<ScrollToIndexMessage>
{
    protected private TextoViewModel _textoViewModel;
    public TextoView(TextoViewModel textoViewModel)
    {
        InitializeComponent();
        BindingContext = textoViewModel;
        _textoViewModel = textoViewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _textoViewModel.InitializeAsync();
    }

    void IRecipient<ScrollToIndexMessage>.Receive(ScrollToIndexMessage message)
    {
        int index = message.Index;
        collectionView.ScrollTo(index, position: ScrollToPosition.Start, animate: true);
    }
}