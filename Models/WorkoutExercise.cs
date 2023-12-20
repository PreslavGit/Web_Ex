using webex.Models;

namespace webex.Models;

public class WorkoutExercise 
{
    public int Id { get; set; }
    public int? Reps { get; set; }
    public decimal? Weight { get; set; }
    public int? Duration { get; set; }
    public int? Rest_Seconds { get; set; }
    
    public Workout Workout { get; set; } = new(); 
    public Exercise? Exercise { get; set; }
}