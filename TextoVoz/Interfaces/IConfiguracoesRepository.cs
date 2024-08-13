using TextoVoz.Mvvm.Models;

namespace TextoVoz.Interfaces
{
    public interface IConfiguracoesRepository
    {

        public Task<Configuracoes> GetConfiguracoesAsync();

        public void UpdateConfiguracoes(Configuracoes config);

        public Task<List<Locale>> GetVozlocaisAsync();
    }
}
