namespace CalmingEssenceTherapies.App.Models
{
    public class Treatment
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
    }
}
