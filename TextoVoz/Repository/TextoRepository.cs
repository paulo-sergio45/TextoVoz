using System.Text.Json;
using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Repository
{

    class TextoRepository : ITextoRepository
    {
        public string FileName { get; } = "/TextoStore.txt";

        public int Index { get; }

        public string Path { get; } = FileSystem.AppDataDirectory;

        public async Task<Texto> GetTexto()
        {
            try
            {
                if (!File.Exists(Path + FileName))
                    File.Create(Path + FileName).Close();

                var rawData = await File.ReadAllTextAsync(Path + FileName);

                if (string.IsNullOrEmpty(rawData))
                    return new Texto();

                var texto = JsonSerializer.Deserialize<Texto>(rawData);

                if (texto != null)
                    return texto;
            }
            catch (Exception)
            {
                throw;
            }
            return new Texto();
        }
        public int GetIndex()
        {
            try
            {
                int index = Preferences.Default.Get("Index", 0);
                return index;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async void UpdateTexto(Texto texto)
        {
            try
            {
                var serializedData = JsonSerializer.Serialize(texto);
                await File.WriteAllTextAsync(Path + FileName, serializedData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateIndex(int index)
        {
            try
            {
                Preferences.Default.Set("Index", index);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
