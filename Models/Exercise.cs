using webex.Models;

public class Exercise {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    public List<MuscleGroup> MuscleGroups { get; } = new();
}