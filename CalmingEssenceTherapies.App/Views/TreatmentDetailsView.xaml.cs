using CalmingEssenceTherapies.App.ViewModels;

namespace CalmingEssenceTherapies.App.Views;

public partial class TreatmentDetailsView : ContentPage
{
    public TreatmentDetailsView(TreatmentDetailsViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}