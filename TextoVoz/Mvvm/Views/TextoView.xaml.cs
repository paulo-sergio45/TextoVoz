using TextoVoz.Mvvm.ViewModels;

namespace TextoVoz.Mvvm.Views;

public partial class TextoView : ContentPage
{
    public TextoView(TextoViewModel textoViewModel)
    {
        InitializeComponent();
        BindingContext = textoViewModel;
        textoViewModel.IndexViewModel = (value) => collectionView.ScrollTo(value, position: ScrollToPosition.Start, animate: true);
    }
}