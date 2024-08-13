using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Mvvm.Views;

public partial class HomeView : ContentPage
{
    private readonly ICustomFileTypeService _customFileTypeText;
    private readonly ITextoRepository _textoRepository;

    public HomeView(ICustomFileTypeService customFileType, ITextoRepository textoRepository)
    {
        InitializeComponent();
        _customFileTypeText = customFileType;
        _textoRepository = textoRepository;

    }

    public async void selecioneArquivo_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.Default.PickAsync(_customFileTypeText.GetCustomFileTypeText("selecione um arquivo .txt"));

        if (result != null)
        {
            _textoRepository.UpdateTexto(new Texto { Linhas = readFile(result) });
            await Shell.Current.GoToAsync($"//{nameof(TextoView)}?Reload=true");
        }
    }

    private List<string> readFile(FileResult fileResult)
    {
        try
        {
            var texto = File.ReadAllLines(fileResult.FullPath).ToList();

            if (texto != null)
                return texto;
        }
        catch (Exception)
        {
            throw;
        }
        return [];
    }



}