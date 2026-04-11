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

                if (texto == null)
                    return new Texto();

                var textoIdSalvo = Preferences.Default.Get("NomeArquivo", "");
                texto.NomeArquivo = textoIdSalvo;

                return texto;
            }
            catch (Exception)
            {
                throw;
            }
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
        public async Task UpdateTexto(Texto texto)
        {
            try
            {
                var textoIdSalvo = Preferences.Default.Get("NomeArquivo", "");

                if (textoIdSalvo != texto.NomeArquivo)
                {
                    UpdateIndex(0);
                }

                var serializedData = JsonSerializer.Serialize(texto);
                await File.WriteAllTextAsync(Path + FileName, serializedData);
                Preferences.Default.Set("NomeArquivo", texto.NomeArquivo);

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
