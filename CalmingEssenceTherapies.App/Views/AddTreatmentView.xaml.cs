using CalmingEssenceTherapies.App.ViewModels;
using System.Globalization;

namespace CalmingEssenceTherapies.App.Views;

public partial class AddTreatmentView : ContentPage
{
    public AddTreatmentView(AddTreatmentViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }

    private void PriceEntry_Focused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry && decimal.TryParse(entry.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out var value))
        {
            entry.Text = value.ToString("0.##");
            if (entry.Text == "0")
            {
                entry.Text = "";
            }
        }
    }

    private void PriceEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (sender is Entry entry && decimal.TryParse(entry.Text, out var value))
        {
            entry.Text = value.ToString("C2");
        }
    }
}