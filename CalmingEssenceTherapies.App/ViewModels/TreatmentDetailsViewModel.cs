using CalmingEssenceTherapies.App.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalmingEssenceTherapies.App.ViewModels;

public partial class TreatmentDetailsViewModel : ObservableObject
{

    private readonly ITreatmentService _treatmentService;

    [ObservableProperty]
    public partial int Id { get; set; }

    [ObservableProperty]
    public partial string Name { get; set; } = "";

    [ObservableProperty]
    public partial string? Description { get; set; }

    [ObservableProperty]
    public partial decimal Price { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    public TreatmentDetailsViewModel(ITreatmentService treatmentService)
    {
        _treatmentService = treatmentService;
    }

    [RelayCommand]
    public async Task Appearing()
    {
        await Refresh();
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
            var treatmentDetails = await _treatmentService.GetTreatmentDetails(1);
            if (treatmentDetails == null)
            {
                Console.WriteLine("Treatment details not found.");
                return;
            }
            Id = treatmentDetails.Id;
            Name = treatmentDetails.Name;
            Description = treatmentDetails.Description;
            Price = treatmentDetails.Price;
            Console.WriteLine("Success!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
