using System.Collections.Generic;
using System.IO;
using System.Linq;
using GymBuddy.Entities;
using Environment = System.Environment;

namespace GymBuddy
{
    internal class FileHelper
    {
        private static readonly string WorkoutFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "myworkouts.json");

        public static List<Workout> GetWorkoutList()
        {
            if (!File.Exists(WorkoutFile)) return new List<Workout>();
            var workouts = File.ReadAllText(WorkoutFile);
            return ListConverter.ConvertStringToList<Workout>(workouts) ?? new List<Workout>();
        }

        public static Workout GetWorkout(string name)
        {
            var workouts = GetWorkoutList();
            return workouts.First(workout => workout.Name == name);
        }

        public static void UpdateWorkout(Workout workout)
        {
            var workouts = GetWorkoutList();
            var index = workouts.FindIndex(w => w.Name == workout.Name);
            if (index <= -1 || index >= workouts.Count) return;
            workouts[index] = workout;
            WriteWorkoutList(workouts);
        }

        public static void AddWorkout(Workout workout)
        {
            if (workout == null) return;
            var workouts = GetWorkoutList();
            workouts.Add(workout);
            WriteWorkoutList(workouts);
        }

        public static void RemoveWorkout(Workout workout)
        {
            if (workout == null) return;
            var workouts = GetWorkoutList();
            var index = workouts.FindIndex(w => w.Name == workout.Name);
            if (index <= -1 || index >= workouts.Count) return;
            workouts.RemoveAt(index);
            WriteWorkoutList(workouts);
        }

        private static void WriteWorkoutList(IList<Workout> workouts)
        {
            if (workouts != null)
            {
                File.WriteAllText(WorkoutFile, ListConverter.ConvertListToString(workouts));
            }
        }
    }
}