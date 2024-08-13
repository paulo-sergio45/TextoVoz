using TextoVoz.Mvvm.Models;

namespace TextoVoz.Interfaces
{
    public interface ITextoService
    {
        public Texto GetTexto();

        public void AtualizaTexto(Texto texto);
    }
}
