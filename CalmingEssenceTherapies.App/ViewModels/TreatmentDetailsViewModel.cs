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
            Console.WriteLine("Success!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
