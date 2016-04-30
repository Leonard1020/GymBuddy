using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using GymBuddy.Entities;

namespace GymBuddy.Adapters
{
    class ExerciseListAdapter : BaseAdapter<IExercise>
    {
        private readonly List<IExercise> _exercises;
        private readonly Context _context;

        public ExerciseListAdapter(Context c, List<IExercise> e)
        {
            _exercises = e;
            _context = c;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.ExerciseListViewRow, parent, false);

            var name = row.FindViewById<TextView>(Resource.Id.exercise_name);

            var exercise = _exercises[position];

            name.Text = exercise.Name;

            return row;
        }

        public override int Count => _exercises?.Count ?? 0;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override IExercise this[int position] => _exercises[position];
    }
}