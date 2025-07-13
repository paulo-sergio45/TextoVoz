using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;
using TextoVoz.Mvvm.Views;

namespace TextoVoz.Mvvm.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ICustomFileTypeService _customFileTypeText;

        private readonly ITextoRepository _textoRepository;
        public HomeViewModel(ICustomFileTypeService customFileType, ITextoRepository textoRepository)
        {

            _customFileTypeText = customFileType;
            _textoRepository = textoRepository;
        }

        [RelayCommand]
        private async Task SelecionarArquivoAsync()
        {
            var result = await FilePicker.Default.PickAsync(_customFileTypeText.GetCustomFileTypeText("selecione um arquivo .txt"));

            if (result != null)
            {
                _textoRepository.UpdateTexto(new Texto { Linhas = ReadFile(result) });
                await Shell.Current.GoToAsync($"//{nameof(TextoView)}");
            }
        }

        private static List<string> ReadFile(FileResult fileResult)
        {
            try
            {
                var texto = File.ReadAllLines(fileResult.FullPath).ToList();

                if (texto != null)
                    return texto;
            }
            catch (Exception)
            {
                throw new Exception("O arquivo está vazio.");
            }
            return new List<string>();
        }
    }


}
