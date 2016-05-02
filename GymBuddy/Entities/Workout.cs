using System;
using System.Collections.Generic;

namespace GymBuddy.Entities
{
    class Workout
    {
        public string Name { get; }
        public string Creator { get; }
        public DateTime? LastCompleted { get; }
        public List<IExercise> Exercises { get; set; }

        public Workout(string name, string creator, DateTime? completeDate, List<IExercise> exercises)
        {
            Name = name ?? "Nameless Workout";
            Creator = creator;
            LastCompleted = completeDate;
            Exercises = exercises ?? new List<IExercise>();
        }
    }
}