using TextoVoz.Mvvm.Models;

namespace TextoVoz.Interfaces
{
    public interface ITextoRepository
    {
        public Texto GetTexto();

        public void UpdateTexto(Texto texto);
    }
}
