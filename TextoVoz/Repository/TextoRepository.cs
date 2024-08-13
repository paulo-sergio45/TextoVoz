using System.Text.Json;
using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;

namespace TextoVoz.Repository
{


    class TextoRepository : ITextoRepository
    {
        public string FileName { get; } = "/TextoStore.txt";
        public string Path { get; } = FileSystem.AppDataDirectory;

        public Texto GetTexto()
        {
            try
            {

                if (!File.Exists(Path + FileName))
                {
                    File.Create(Path + FileName).Close();
                }

                var rawData = File.ReadAllText(Path + FileName);

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

        public void UpdateTexto(Texto texto)
        {
            try
            {
                var serializedData = JsonSerializer.Serialize(texto);
                File.WriteAllText(Path + FileName, serializedData);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
