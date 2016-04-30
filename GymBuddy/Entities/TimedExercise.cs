using System;

namespace GymBuddy.Entities
{
    class TimedExercise : IExercise
    {
        public string Name { get; }
        public TimeSpan Time { get; }
        public IExercise Exercise { get; }

        public TimedExercise(IExercise exercise, TimeSpan time, string name = null)
        {
            if (name != null)
            {
                Name = name;
            }
            else
            {
                Name = exercise != null ? $"Timed {exercise.Name}" : "Timed Exercise";
            }
            Time = time;
            Exercise = exercise;
        }
    }
}