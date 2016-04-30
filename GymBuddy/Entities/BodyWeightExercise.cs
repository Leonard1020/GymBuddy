namespace GymBuddy.Entities
{
    class BodyWeightExercise : IExercise
    {
        public string Name { get; protected set; }
        public int Sets { get; protected set; }
        public int Reps { get; protected set; }
        public int Pr { get; protected set; } //Personal Record

        public BodyWeightExercise(string name, int sets, int reps, int pr = 0)
        {
            Name = name ?? "Nameless Body Weight Exercise";
            Sets = sets > -1 ? sets : 0;
            Reps = reps > -1 ? reps : 0;
            Pr = pr > -1 ? pr : 0;
        }
    }
}