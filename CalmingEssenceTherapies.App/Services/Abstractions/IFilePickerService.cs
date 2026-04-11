namespace CalmingEssenceTherapies.App.Services.Abstractions
{
    public interface IFilePickerService
    {
        Task<FileResult?> PickImageAsync();
    }
}
