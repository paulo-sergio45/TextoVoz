using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Service
{
    public class ConfiguracoesService(IConfiguracoesRepository configuracoesRepository) : IConfiguracoesService
    {
        private readonly IConfiguracoesRepository _configuracoesRepository = configuracoesRepository;

        public async Task<Configuracoes> GetConfiguracoes()
        {
            return await _configuracoesRepository.GetConfiguracoesAsync();
        }

        public void AtualizaConfiguracoes(Configuracoes config)
        {
            _configuracoesRepository.UpdateConfiguracoes(config);
        }

        public async Task<List<Locale>> GetVozlocaisAsync()
        {
            return await _configuracoesRepository.GetVozlocaisAsync();
        }
    }
}
