namespace GymBuddy.Entities
{
    class SuperSet : IExercise
    {
        public string Name { get; }
        public IExercise First { get; }
        public IExercise Second { get; }
        public int Cycles { get; }

        public SuperSet(string name, int iterations, IExercise first, IExercise second)
        {
            Name = name ?? "Nameless Super Set";
            Cycles = iterations > -1 ? iterations : 0;
            First = first;
            Second = second;
        }
    }
}