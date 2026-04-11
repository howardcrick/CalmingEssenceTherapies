namespace CalmingEssenceTherapies.Data.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Treatment> Treatments { get; } = new List<Treatment>();
}