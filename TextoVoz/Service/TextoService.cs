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

        public int GetIndex()
        {
            return _textoRepository.GetIndex();
        }

        public async Task<Texto> GetTexto()
        {
            return await _textoRepository.GetTexto();
        }

        public void UpdateIndex(int index)
        {
            _textoRepository.UpdateIndex(index);
        }
    }
}
