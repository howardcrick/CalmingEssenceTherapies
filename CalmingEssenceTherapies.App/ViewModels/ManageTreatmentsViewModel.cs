using CalmingEssenceTherapies.App.Models;
using CalmingEssenceTherapies.App.Services.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalmingEssenceTherapies.App.ViewModels
{
    public partial class ManageTreatmentsViewModel : ObservableObject
    {
        private readonly ITreatmentService _treatmentService;
        public ManageTreatmentsViewModel(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [ObservableProperty]
        public partial List<ManageTreatment> Treatments { get; set; } = new List<ManageTreatment>();

        [ObservableProperty]
        public partial ManageTreatment? SelectedTreatment { get; set; }

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
            await Shell.Current.GoToAsync("AddTreatmentView");
        }

        [RelayCommand]
        public async Task EditTreatment()
        {
            if (SelectedTreatment is null) return;

            await Shell.Current.GoToAsync($"TreatmentDetailsView?id={SelectedTreatment.Id}");

            SelectedTreatment = null;
        }

        private async Task LoadData()
        {
            try
            {
                var allTreatments = await _treatmentService.GetAllTreatments();
                if (allTreatments == null)
                {
                    Console.WriteLine("Treatments not found.");
                    return;
                }
                Treatments = allTreatments;
                Console.WriteLine("Get treatments success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}

