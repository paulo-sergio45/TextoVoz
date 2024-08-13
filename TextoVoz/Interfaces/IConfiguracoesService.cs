using TextoVoz.Mvvm.Models;

namespace TextoVoz.Interfaces
{
    public interface IConfiguracoesService
    {
        public Task<Configuracoes> GetConfiguracoes();

        public void AtualizaConfiguracoes(Configuracoes config);

        public Task<List<Locale>> GetVozlocaisAsync();
    }
}
