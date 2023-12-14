using Microsoft.AspNetCore.Identity;

public class Workout {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Color { get; set; } = "";
    
    public IdentityUser User { get; set; } =  new();
    public List<WorkoutExercise>? WorkoutExercises { get; set; } 
}