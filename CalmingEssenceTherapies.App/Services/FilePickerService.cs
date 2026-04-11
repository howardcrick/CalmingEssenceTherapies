using CalmingEssenceTherapies.App.Services.Abstractions;

namespace CalmingEssenceTherapies.App.Services
{
    public class FilePickerService : IFilePickerService
    {
        public async Task<FileResult?> PickImageAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an image",
                    FileTypes = FilePickerFileType.Images
                });
                if (result != null)
                    return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File picking failed: {ex.Message}");
            }
            return null;
        }
    }
}
