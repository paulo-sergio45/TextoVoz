using TextoVoz.Mvvm.Models;

namespace TextoVoz.Interfaces
{
    public interface ITextoService
    {
        public Task<Texto> GetTexto();

        public Task AtualizaTexto(Texto texto);

        public void UpdateIndex(int index);

        public int GetIndex();
    }
}
