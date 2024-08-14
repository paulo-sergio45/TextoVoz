using TextoVoz.Interfaces;

namespace TextoVoz.Service.Helpers
{
    public class CustomFileTypeService : ICustomFileTypeService
    {
        public PickOptions Options { get; set; }

        public CustomFileTypeService()
        {
            Options = new PickOptions();
        }

        public PickOptions GetCustomFileTypeText(string text)
        {
            Options = new PickOptions
            {
                PickerTitle = text,
                FileTypes = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            { DevicePlatform.iOS, ["public.text"] }, // UTType values
                            { DevicePlatform.Android, ["text/plain"] }, // MIME type
                            { DevicePlatform.WinUI, [".Txt"] }, // file extension
                            { DevicePlatform.Tizen, ["*/*"] },
                            { DevicePlatform.macOS, ["Txt"] }, // UTType values
                        })
            };
            return Options;
        }

    }
}