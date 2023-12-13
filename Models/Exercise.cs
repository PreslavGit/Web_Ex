using webex.Models;

public class Exercise {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Color { get; set; } = "";
    public List<MuscleGroup> MuscleGroups { get; set;} = new();
}