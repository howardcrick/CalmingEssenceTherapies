using CalmingEssenceTherapies.App.Helpers;
using CalmingEssenceTherapies.App.Models;
using CalmingEssenceTherapies.App.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalmingEssenceTherapies.App.ViewModels;

public partial class TreatmentDetailsViewModel : ObservableObject, IQueryAttributable
{
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        int? id = int.TryParse(query["id"].ToString(), out var parsedId) ? parsedId : null;
        if (id == null)
        {
            Console.WriteLine("Invalid treatment ID.");
            return;
        }
        Id = id.Value;
        await Refresh();
    }

    private readonly ITreatmentService _treatmentService;

    private int Id;

    [ObservableProperty]
    public required partial string Name { get; set; }

    [ObservableProperty]
    public partial string? Description { get; set; }

    [ObservableProperty]
    public partial decimal Price { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ChangeTreatmentImageText))]
    public partial string? ImageUrl { get; set; }

    [ObservableProperty]
    public required partial Category SelectedCategory { get; set; }

    [ObservableProperty]
    public partial int DurationHours { get; set; }

    [ObservableProperty]
    public partial int DurationMinutes { get; set; }

    private int Duration => (DurationHours * 60) + DurationMinutes;

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }


    public string ChangeTreatmentImageText => ImageUrl == null ? "Add Image" : "Replace Image";

    public TreatmentDetailsViewModel(ITreatmentService treatmentService)
    {
        _treatmentService = treatmentService;
    }

    [RelayCommand]
    private async Task Refresh()
    {
        try
        {
            IsRefreshing = true;
            await LoadData();
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private async Task LoadData()
    {
        try
        {
            var treatmentDetails = await _treatmentService.GetTreatmentDetails(Id);
            if (treatmentDetails == null)
            {
                Console.WriteLine("Treatment details not found.");
                return;
            }
            Name = treatmentDetails.Name;
            Description = treatmentDetails.Description;
            Price = treatmentDetails.Price;
            ImageUrl = treatmentDetails.ImageUrl?.GetImageUrl();
            DurationHours = treatmentDetails.Duration / 60;
            DurationMinutes = treatmentDetails.Duration % 60;
            SelectedCategory = new Category
            {
                Id = treatmentDetails.Category.Id,
                Name = treatmentDetails.Category.Name
            };
            Console.WriteLine("Success!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    [RelayCommand]
    public async Task SaveTreatment()
    {
        try
        {
            await _treatmentService.EditTreatment(Id, Name, Description, Price, SelectedCategory.Id, Duration);
            await ToastHelper.ShowToast("Treatment saved successfully!");
        }
        catch (Exception ex)
        {
            await ToastHelper.ShowToast("Failed to edit treatment. Please try again.");
            Console.WriteLine(ex);
        }
    }
}
