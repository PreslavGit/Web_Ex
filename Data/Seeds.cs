using System.Drawing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webex.Data;
using webex.Models;

public static class Seeds
{
    public async static Task Initialize(DbContextEx context, UserManager<IdentityUser> userManager)
    {
        context.Database.Migrate();

        
        if (context.MuscleGroups.Any() || context.Exercises.Any() || context.Workouts.Any() || context.WorkoutExercises.Any())
        {
            return;
        }

        //User
        var user = new IdentityUser{ Email = "admin@mail.com", UserName = "admin@mail.com" };
        var result = await userManager.CreateAsync(user, "taina_parola_za_ADMIN1");
    
      
      //MuscleGroups
        var musclegroups = new List<MuscleGroup>(){
            new MuscleGroup{ Name="Chest", Location=BodyLocation.UpperBody, MuscleFunction =MuscleFunction.Push,Color= "#4b88c9"},
            new MuscleGroup{Name="Back", Location= BodyLocation.UpperBody, MuscleFunction =MuscleFunction.Pull,Color= "#8ae05c"},
            new MuscleGroup{Name="Quadriceps", Location= BodyLocation.LowerBody, MuscleFunction =MuscleFunction.Push,Color= "#db40c4"},
            new MuscleGroup{Name="Hamstrings", Location= BodyLocation.LowerBody, MuscleFunction =MuscleFunction.Push,Color= "#c2a744"},
            new MuscleGroup{Name="Biceps", Location= BodyLocation.UpperBody, MuscleFunction =MuscleFunction.Pull,Color= "#9443e0"}
        };
        foreach (MuscleGroup m in musclegroups)
        {
            context.MuscleGroups.Add(m);
        }

        //Exercise
        var exercises = new List<Exercise>(){
            new Exercise{Name="Bench Press", MuscleGroups= new List<MuscleGroup>{musclegroups.ElementAt(0)},Color="#e85e38"},
            new Exercise{Name="Pull-Ups", MuscleGroups= new List<MuscleGroup>{musclegroups.ElementAt(1), musclegroups.ElementAt(4)},Color="#fcff96"},
            new Exercise{Name="Squats", MuscleGroups= new List<MuscleGroup>{musclegroups.ElementAt(2)},Color="#40dbd8"},
            new Exercise{Name="Deadlifts", MuscleGroups= new List<MuscleGroup>{musclegroups.ElementAt(3)},Color="#44c253"},
            new Exercise{Name="Bicep Curls", MuscleGroups= new List<MuscleGroup>{musclegroups.ElementAt(4)},Color="#db9d32"},
            new Exercise{Name="Treadmill", MuscleGroups= new List<MuscleGroup>{musclegroups.ElementAt(2),musclegroups.ElementAt(3)},Color="#db9d52"}
        };
        foreach(Exercise ex in exercises){
            context.Exercises.Add(ex);
        }

        //Workout
        var workouts = new List<Workout>(){
            new Workout{Name="Monday", User= user, Color="#582f78"},
            new Workout{Name= "Tuesday", User=user, Color= "#364187"}
        };
        foreach (Workout wo in workouts)
        {
            context.Workouts.Add(wo);
        }

        //WorkoutExercise
        var workoutExercises = new List<WorkoutExercise>(){ 
            new WorkoutExercise{Reps=10, Weight=135, Rest_Seconds=60,Workout=workouts.ElementAt(0),Exercise=exercises.ElementAt(0)},
            new WorkoutExercise{Duration=50, Rest_Seconds=30,Workout=workouts.ElementAt(0),Exercise=exercises.ElementAt(5)},
            new WorkoutExercise{Reps=15, Weight=30, Rest_Seconds=45,Workout=workouts.ElementAt(1),Exercise=exercises.ElementAt(4)},
            new WorkoutExercise{Duration=30, Rest_Seconds=20,Workout=workouts.ElementAt(1),Exercise=exercises.ElementAt(5)}
        };

        foreach (WorkoutExercise we in workoutExercises)
        {
            context.WorkoutExercises.Add(we);
        };
        context.SaveChanges();
    }
}
