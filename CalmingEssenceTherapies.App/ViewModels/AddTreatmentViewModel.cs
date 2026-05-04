using CalmingEssenceTherapies.App.Helpers;
using CalmingEssenceTherapies.App.Models;
using CalmingEssenceTherapies.App.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalmingEssenceTherapies.App.ViewModels;

public partial class AddTreatmentViewModel : ObservableObject
{
    private readonly ICategoryService _categoryService;
    private readonly ITreatmentService _treatmentService;
    private readonly IFilePickerService _filepickerService;

    [ObservableProperty]
    public partial int Id { get; set; }

    [ObservableProperty]
    public partial string Name { get; set; } = "";

    [ObservableProperty]
    public partial string? Description { get; set; }

    [ObservableProperty]
    public partial decimal Price { get; set; }

    [ObservableProperty]
    public partial List<Category> Categories { get; set; } = new List<Category>();

    [ObservableProperty]
    public partial Category? SelectedCategory { get; set; }

    [ObservableProperty]
    public partial int DurationHours { get; set; }

    [ObservableProperty]
    public partial int DurationMinutes { get; set; }

    private int Duration => (DurationHours * 60) + DurationMinutes;

    private int SelectedCategoryId => SelectedCategory?.Id ?? 0;

    private FileResult? TreatmentImage { get; set; }

    [ObservableProperty]
    public partial string? TreatmentImageFileName { get; set; }

    public AddTreatmentViewModel(ICategoryService categoryService, ITreatmentService treatmentService, IFilePickerService filePickerService)
    {
        _categoryService = categoryService;
        _treatmentService = treatmentService;
        _filepickerService = filePickerService;
    }

    [RelayCommand]
    public async Task Appearing()
    {
        await Refresh();
    }

    [RelayCommand]
    private async Task Refresh()
    {
        await LoadData();
    }

    [RelayCommand]
    public async Task AddTreatment()
    {
        try
        {
            await _treatmentService.AddTreatment(Name, Description, Price, SelectedCategoryId, Duration, TreatmentImage);
            await ToastHelper.ShowToast("Treatment added successfully!");
        }
        catch (Exception ex)
        {
            await ToastHelper.ShowToast("Failed to add treatment. Please try again.");
            Console.WriteLine(ex);
        }
        finally
        {
            ClearFields();
        }
    }

    [RelayCommand]
    public async Task SelectTreatmentImage()
    {
        TreatmentImage = await _filepickerService.PickImageAsync();
        TreatmentImageFileName = TreatmentImage?.FileName;
    }

    private async Task LoadData()
    {
        try
        {
            var allCategories = await _categoryService.GetCategories();
            if (allCategories == null)
            {
                Console.WriteLine("Treatment details not found.");
                return;
            }
            Categories = allCategories;
            Console.WriteLine("Get categories success!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void ClearFields()
    {
        Name = string.Empty;
        Description = null;
        Price = 0;
        SelectedCategory = null;
        TreatmentImage = null;
        TreatmentImageFileName = null;
    }

}
