using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Service
{
    public class TextoService : ITextoService
    {
        private readonly ITextoRepository _textoRepository;

        public TextoService(ITextoRepository textoRepository)
        {
            _textoRepository = textoRepository;
        }
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
