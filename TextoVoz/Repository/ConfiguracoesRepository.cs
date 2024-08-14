using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Repository
{
    public class ConfiguracoesRepository : IConfiguracoesRepository
    {
        public async Task<Configuracoes> GetConfiguracoesAsync()
        {
            double volume = Preferences.Default.Get("ConfigVolume", 0.0);
            double tom = Preferences.Default.Get("configTom", 0.0);
            string localName = Preferences.Default.Get("configLocal", "");

            List<Locale> locales = await GetVozlocaisAsync();
            var local = locales.FirstOrDefault(e => e.Name == localName) ?? locales.First();

            Configuracoes config = new() { Tom = tom, Volume = volume, Local = local };

            return config;
        }
        public void UpdateConfiguracoes(Configuracoes config)
        {
            if (config.Local != null)
            {
                Preferences.Default.Set("ConfigVolume", config.Volume);
                Preferences.Default.Set("configTom", config.Tom);
                Preferences.Default.Set("configLocal", config.Local.Name);
            }
        }

        public async Task<List<Locale>> GetVozlocaisAsync()
        {
            IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();
            return locales.ToList();
        }
    }
}
