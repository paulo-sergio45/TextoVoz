using TextoVoz.Interfaces;
using TextoVoz.Mvvm.ViewModels;

namespace TextoVoz.Mvvm.Views;

public partial class TextoView : ContentPage
{
    private readonly ITextoRepository _textoRepository;


    public TextoView(ITextoRepository textoRepository, TextoViewModel textoViewModel)
    {
        InitializeComponent();

        _textoRepository = textoRepository;

        BindingContext = textoViewModel;
        textoViewModel.IndexViewModel = (value) => collectionView.ScrollTo(value, position: ScrollToPosition.Start, animate: true);

    }

}