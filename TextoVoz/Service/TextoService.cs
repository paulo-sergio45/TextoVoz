using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Service
{
    public class TextoService(ITextoRepository textoRepository) : ITextoService
    {
        private readonly ITextoRepository _textoRepository = textoRepository;

        public void AtualizaTexto(Texto texto)
        {
            _textoRepository.UpdateTexto(texto);
        }

        public Texto GetTexto()
        {
            return _textoRepository.GetTexto();
        }
    }
}
