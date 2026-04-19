using CalmingEssenceTherapies.App.ViewModels;

namespace CalmingEssenceTherapies.App.Views;

public partial class ManageTreatmentsView : ContentPage
{
    public ManageTreatmentsView(ManageTreatmentsViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}