namespace webex.Models;

public enum BodyLocation 
{
   UpperBody,
   LowerBody 
}

public enum MuscleFunction 
{
   Push,
   Pull 
}

public class MuscleGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public BodyLocation Location { get; set; }
    public MuscleFunction MuscleFunction { get; set; }
    public required string Color { get; set; }
    public List<Exercise> Exercises { get; } = new();
}