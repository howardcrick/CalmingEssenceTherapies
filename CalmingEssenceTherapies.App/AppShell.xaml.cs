using CalmingEssenceTherapies.App.Views;

namespace CalmingEssenceTherapies.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("AddTreatmentView", typeof(AddTreatmentView));
            Routing.RegisterRoute("TreatmentDetailsView", typeof(TreatmentDetailsView));
        }
    }
}
