using TextoVoz.Mvvm.Models;

namespace TextoVoz.Interfaces
{
    public interface ITextoRepository
    {
        public Task<Texto> GetTexto();

        public Task UpdateTexto(Texto texto);

        public void UpdateIndex(int index);

        public int GetIndex();
    }
}
