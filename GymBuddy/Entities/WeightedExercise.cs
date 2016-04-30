using Newtonsoft.Json;

namespace GymBuddy.Entities
{
    class WeightedExercise : BodyWeightExercise
    {
        public int? StartingWeight { get; }
        public int WeightIncrements { get; }

        public WeightedExercise(string name, int sets, int reps) : this(name, sets, reps, 0) { }

        public WeightedExercise(string name, int sets, int reps, int startingWeight) : this(name, sets, reps, startingWeight, 0) { }

        [JsonConstructor]
        public WeightedExercise(string name, int sets, int reps, int startingWeight, int weightIncrements) : base(name, sets, reps)
        {
            Name = name ?? "Nameless Weighted Exercise";
            WeightIncrements = weightIncrements;
            StartingWeight = startingWeight > -1 ? startingWeight : 0;
        }
    }
}